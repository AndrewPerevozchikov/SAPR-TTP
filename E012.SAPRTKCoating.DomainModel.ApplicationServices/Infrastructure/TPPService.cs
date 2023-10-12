using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E012.SAPRTKCoating.EFDataSource.DBContext;
using E012.DomainModelServer.Model.Entities.SKAT;

namespace E012.SAPRTKCoating.DomainModel.ApplicationServices.Infrastructure
{
    public class TPPService : IDisposable
    {
        private LocalContextSKAT _context = null;
        
        public LocalContextSKAT LocalContext
        {
            get
            {
                if (_context == null)
                    _context = new LocalContextSKAT();
                return _context;
            }
        }
        /// <summary>
        /// Процедура получения дерева ТТП по дсе и виду технологий
        /// </summary>
        /// <param name="name_dse">ДСЕ</param>
        /// <param name="type_work">вид технологии</param>
        /// <returns></returns> 
        public List<TTPForRead> GetTreeTP(string name_dse, string type_work)
        {
            return LocalContext.GetTreeTP(name_dse, type_work).ToList();
        }
        /// <summary>
        /// получение данных по типу из дерева ТТП
        /// </summary>
        /// <param name="name_dse">ДСЕ</param>
        ///  <param name="type_work">вид работы</param>
        ///  <param name="level">уровень в дереве</param>
        ///  <param name="Tip">тип переменной (пункт дерева)</param>
        ///  <param name="version">версия документа</param>
        ///  <param name="id_operationeration">идентификатор  операции </param>
        ///  <param name="number_trek">номер перехода (0- все, иначе конкретный)</param>
        ///  <param name="pornom">порядковый номер (0- все, иначе конкретный)</param>
        ///  <param name="attribute_single">признак подписи (0-автор, 1-соисполнители, 2- согласующие)</param>
        ///  <param name="attribute_doc">признак документа (1-ТП,2-извещение)</param>
        ///  <param name="id_table">идентификатор таблицы</param>
        /// <returns></returns>
        /// 
        public List<T> GetTIPTreeTP<T>(string name_dse, string type_work, int tip, int level, Guid id_operationeration, string number_trek, int pornom, int attribute_single, int attribute_doc, Guid id_table)
        {
            return LocalContext.GetTIPTreeTP<T>(name_dse, type_work,tip,level,id_operationeration,number_trek,pornom,attribute_single,attribute_doc,id_table).ToList(); 
        }
        public List<T> GetListMethodTransaction<T>(short nom_mas)
        {
            return LocalContext.GetListMethodTransaction<T>(nom_mas).ToList();
        }
        #region Add
        /// <summary>
        /// метод принимает всю информацию о операциях и режим работы программы(прооектировани или  коррекция)
        /// </summary>
        /// <param name="information"></param>
        /// <param name="correction"></param>
        public void StoreChangeOperation(List<OperationInformation> information, bool correction)
        {
            foreach (var item in information)
            {
                string version = "00";
                if (correction)
                {
                    var newversion = LocalContext.GetversionNew(item.operation.name_dse, item.operation.type_work).FirstOrDefault();
                    version= LocalContext.GetversionNow(item.operation.name_dse, item.operation.type_work, (short)item.operation.number_operation).FirstOrDefault();
                    if (version != newversion)
                        LocalContext.Updateversion(newversion, item.operation.id_operation);
                    version = newversion;
                }
                Store(item.operation, version);
                StoreData(item.equipments,version);
                StoreData(item.materials, version);
                StoreData(item.riggings, version);
                StoreData(item.modes, version);
                StoreData(item.attention, version);
                StoreData(item.instruction, version);
                if (item.trekInformation != null)
                {
                    foreach (var trek in item.trekInformation)
                    {
                        Store(trek.trek, version);
                        StoreData(trek.riggings, version);
                        StoreData(trek.modes, version);
                        StoreData(trek.freeTextTreks, version);
                    }
                }
                
                var login = LocalContext.GetLogins(item.operation).FirstOrDefault();
                if (login == null)
                    LocalContext.StoreData(item.operation,false);
                else if(item.operation.idavtor!= login.idavtor)
                    LocalContext.StoreData(item.operation, true);
            }
        }
        public void StoreChangeFreeText(FreeText information, bool correction)
        {
            string version = "00";
            if (correction)
                version = GetNewversion(information.version);
            switch((short)information.typeState)
            {
                case 1:
                    if (correction)
                    {
                        var temp = LocalContext.SearchNullFreeText(information).FirstOrDefault();
                        if (temp != null)
                            LocalContext.DeletNullFreeText(information);
                    }
                    LocalContext.StoreData(information, version);
                    break;
                case 2:
                    LocalContext.StoreData(information, version);
                    break;
                case 3:
                    LocalContext.StoreData(information, version);
                    if (correction)
                    {
                        var temp = LocalContext.SearchNullFreeText(information).FirstOrDefault();//????????
                        if (!temp.st.Contains('A'))
                            LocalContext.InsertNullFreeText(information);
                    }
                    break;
            }
        }
        public void StoreTotalInformation(TotalInformation information)
        {
            var newversion = GetNewversion(information.version);
            LocalContext.StoreTotalInformation(information, newversion);
        }
        public void StoreChangePermission(Permission information, bool correction)
        {
            string version = "00";
            if(correction)
                version = GetNewversion(information.TT.version);
  
            if(information.TT!=null)
                StoreTTTB(information.TT, version);
            if (information.TTB != null)
                StoreTTTB(information.TTB, version);
            if (information.TAB != null)
                StoreTTTB(information.TAB, version);
        }
        private void StoreTTTB(Tttb tttb,string version)
        {
            switch ((short)tttb.typeState)
            {
                case 1:
                    var temp = LocalContext.SearchNullTTTB(tttb).FirstOrDefault();//поиск анулированных данных
                    if (temp != null)
                        LocalContext.DeleteNullTTTB(tttb);//если нашли- удаляем
                    LocalContext.StoreTTTB(tttb, version);
                    break;
                case 2:
                    LocalContext.StoreTTTB(tttb, version);
                    break;
                case 3:
                    LocalContext.StoreTTTB(tttb, version);
                    LocalContext.AddNullTttb(tttb);
                    break;
            }
            UpdateAvtor(tttb, version);
            LocalContext.Updateversion(tttb, version);
        }
        private void UpdateAvtor(Tttb tttb,string version)
        {
            var user = LocalContext.SearchAvtor(tttb).FirstOrDefault();//поиск записи о авторе
            if (user != null)
            {
                if (user.idavtor != tttb.idavtor)
                    LocalContext.UpdateTTTB(tttb, version);//если автор существует и он не совпадает с новым автором-перезаписываем
            }
            else
                LocalContext.AddTTTB(tttb, version);//иначе добавляем aвтора
        }
        private string GetNewversion(string version) 
        {
            string result= version;
            if( int.TryParse(version,out int intversion))
            {
                if (intversion < 10)
                    result= "0" + (intversion + 1).ToString();
                else
                    result = (intversion + 1).ToString();
            }
            return result;
        }
        private void StoreData<T>(List<T> data, string version)  where T: AbstractStore
        {
            if (data != null)
            {
                foreach (var t in data)
                {
                    Store(t, version);
                }
            }
        }
        private void Store<T>(T data, string version) where T : AbstractStore
        {
            if (data != null)
            {
                if (data.typeState != 0)
                    LocalContext.StoreData((dynamic)data, version);
            }
        }

        #endregion
        public void AddMethodCreatTtpOrNotice(string name_dse, string type_work, int regim, string fio, string login, string no, string po, int prgrtp)
        {
            LocalContext.AddMethodCreatTtpOrNotice(name_dse, type_work, regim, fio , login, no, po, prgrtp);
        }
            #region IDisposable

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        #endregion
    }
}
