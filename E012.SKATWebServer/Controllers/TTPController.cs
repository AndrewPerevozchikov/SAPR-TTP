using System;
using E012.SAPRTKCoating.DomainModel.ApplicationServices.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using E012.DomainModelServer.Model.Entities.SKAT;
using E012.DomainModelServer.Model.Entities.PCTEXT;
using E012.DomainModelServer.Model.Entities.SKAT.TECHNICAL;
using E012.DomainModelServer.Model.Entities.PCTEXT.Shablone;
using E012.DomainModelServer.Model.Entities;

namespace E012.SKAT.WebServer.Controllers
{
    /// <summary>
    /// Контроллер ТТП
    /// </summary>
    [RoutePrefix("api/TreeTTP")]
    public class TTPController : ApiController
    {
        public TTPController() : base()
        { }

        TPPService tPPService = new TPPService();
        PctextService pctextService = new PctextService();

        #region GET


        #region PCTEXT
        /// <summary>
        /// Процедура получения дерева типовых операций по номеру массива
        /// </summary>
        /// <param name="nom_mas">Номер массива</param>
        /// <param name="operation">операция или переход</param>
        /// <returns></returns>
        [Route("shablone/get")]   ///{nom_mas}/{operation}
        public Result<TreeShablonOperationOrTransit> GetShabloneTree(int nom_mas, bool operation)
        {
            //проверка на правильность параметров
            if (nom_mas <= 0)
            {
                return null;
            }
            try
            {
                 return new Result<TreeShablonOperationOrTransit>(pctextService.GetTreeTP(nom_mas, operation));
            }
            catch (Exception e)
            {
                return new Result<TreeShablonOperationOrTransit>(e.GetBaseException().Message);
            }
        }

        [Route("list_technical_arrays/get")]
        public Result<TechnicalArrays> GetTechnicalArrays()
        {
            try
            {
                return new Result<TechnicalArrays>(pctextService.GetTechnicalArrays());
            }
            catch (Exception e)
            {
                return new Result<TechnicalArrays>(e.GetBaseException().Message);
            }
        }

        #region GETFORTIP

        [Route("tttb_technical/get")]   //можно получить ТТ ТТБ ТЭB зависит от типа
        public Result<TttbTechnicalArrays> GetTTTBInTechnicalArrays(int nom_mas, int Tip)
        {
            return GetTipTechnicalArrays<TttbTechnicalArrays>(nom_mas, Tip,"");
        }
        [Route("operation_technical/get")]   //можно получить операцииа
        public Result<OperationTechnicalArrays> GetOperationInTechnicalArrays(int nom_mas)
        {
            return GetTipTechnicalArrays<OperationTechnicalArrays>(nom_mas, (int)TypeTechnicalArray.OPERATION, "");
        }
        [Route("operation_directory_technical/get")]   //можно получить операцииа
        public Result<OperationDirectory> GetOperationDirectoryInTechnicalArrays()
        {
            return GetTipTechnicalArrays<OperationDirectory>(0, (int)TypeTechnicalArray.OPERATION_DIRECTORY, "");
        }
        [Route("equipment_technical/get")]//можно получить оборудование
        public Result<EquipmentTechnicalArrays> GetEquipmentInTechnicalArrays(int nom_mas,string cex)
        {
            return GetTipTechnicalArrays<EquipmentTechnicalArrays>(nom_mas, (int)TypeTechnicalArray.EQUIPMENT,cex);
        }
        [Route("equipment_directory_technical/get")]//можно получить ОБОРУДОВАНИЕ fullname-- наименование профессии,cex - цех, в котором есть эта профессия
        public Result<EquipmentDirectory> GetEquipmentDirectoryInTechnicalArrays( string cex)
        {
            return GetTipTechnicalArrays<EquipmentDirectory>(0, (int)TypeTechnicalArray.EQUIPMENT_DIRECTORY, cex);
        }
        [Route("material_technical/get")] //можно получить материалы
        public Result<MaterialTechnicalArrays> GetMaterialInTechnicalArrays(int nom_mas)
        {
            return GetTipTechnicalArrays<MaterialTechnicalArrays>(nom_mas, (int)TypeTechnicalArray.MATERIAL, "");
        }
        [Route("material_directory_technical/get")] //можно получить материалы
        public Result<MaterialDirectory> GetMaterialDirectoryInTechnicalArrays(int nom_mas)
        {
            return GetTipTechnicalArrays<MaterialDirectory>(nom_mas, (int)TypeTechnicalArray.MATERIAL_DIRECTORY, "");
        }
        [Route("rigging_technical/get")]//можно получить оснастку
        public Result<RiggingTechnicalArrays> GetRiggingInTechnicalArrays(int nom_mas)
        {
            return GetTipTechnicalArrays<RiggingTechnicalArrays>(nom_mas, (int)TypeTechnicalArray.RIGGING, "");
        }
        [Route("trek_technical/get")]   //можно получить ПЕРЕХОДЫ [О],РЕЖИМЫ [Р],ВНИМАНИЕ [В],УКАЗАНИЕ [У] в зависимости от типа
        public Result<TrekTechnicalArrays> GetTrekInTechnicalArrays(int nom_mas, int Tip)
        {
            return GetTipTechnicalArrays<TrekTechnicalArrays>(nom_mas, Tip, "");
        }
        [Route("Mode_technical/get")]   //можно получить РЕЖИМЫ [Р]
        public Result<ModeTechnicalArrays> GetModeInTechnicalArrays(int nom_mas)
        {
            return GetTipTechnicalArrays<ModeTechnicalArrays>(nom_mas, (int)TypeTechnicalArray.MODE, "");
        }
        #endregion

        #region GETSTANDARTOPERATIONANDTRANSAKTION
        [Route("operation_all/get")]   //можно получить все операции
        public Result<TreeShablone> GetTreeShablone(int nom_mas)
        {
            return GetTree(nom_mas);
        }
        [Route("operation_shablone/get")]   //можно получить класс операции
        public ResultClass<AllOperationInformation> GetShabloneOperation(int nom_mas, Guid id_operation)
        {
            return GetAllShabloneOperation(nom_mas, id_operation,  0);
        }
        [Route("transition_for_tip/get")]   //можно получить класс переходов в операциях по контретному переходу-number_trek
        public ResultClass<AllTrekInformation> GetShabloneTrekInOperation(int nom_mas, Guid id_operation, string number_trek)
        {
            return GetForTipShabloneTransition(nom_mas, id_operation, number_trek, 0);
        }
        [Route("transition_shablone/get")]   //можно получить класс переходов
        public ResultClass<AllTrekInformation> GetShabloneTransition(int nom_mas, Guid id_number_trek)
        {
            return GetAllShabloneTransition(nom_mas, id_number_trek, 0);
        }
        #endregion
        #endregion

        #region SKAT
        /// <summary>
        /// получение дерева ТТП
        /// </summary>
        /// <param name="vidWork"></param>
        /// <param name="dseObozn"></param>
        /// <returns></returns>
        [Route("Get")]   ///{vidWork}/{dseObozn}
        public Result<TTPForRead> GetTreeTTP(string vidWork, string dseObozn) //в начале наименования метода должно стоять Get 
        {
            dseObozn = HttpUtility.UrlDecode(dseObozn);

            Result<TTPForRead> result = null;

            //проверка на правильность параметров
            if (dseObozn.Trim() == "" || (vidWork != "60")) //60-вид работы ТТП
            {
                return null;
            }
            try
            {
                result = GetTreeTtp(dseObozn, vidWork);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.GetBaseException().Message);
            }
        }


        #region GETFORTIP
        /// <summary>
        /// получение данных по типу из дерева ТТП
        /// </summary>
        /// <param name="name_dse">ДСЕ</param>
        ///  <param name="type_work">вид работы</param>
        ///  <param name="level">уровень в дереве</param>
        ///  <param name="version">версия документа</param>
        ///  <param name="id_operation">идентификатор  операции </param>
        ///  <param name="number_trek">номер перехода (0- все, иначе конкретный)</param>
        ///  <param name="pornom">порядковый номер (0- все, иначе конкретный)</param>
        ///  <param name="attribute_single">признак подписи (0-автор, 1-соисполнители, 2- согласующие)</param>
        ///  <param name="attribute_doc">признак документа (1-ТП,2-извещение)</param>
        /// <returns></returns>
        /// 
        /// <summary>
        /// тех требования - главный раздел - не содержит подразделы
        /// </summary>
        short deflevel = 0;
        Guid defid_operation = Guid.Empty;
        string defnumber_trek = "";
        short defpornom = 0;
        short defattribute_single = 0;
        short defattribute_doc = 0;
        Guid defid_table = Guid.Empty;
        [Route("tt_ttb_teb/get")]//вернет один из вариантов согласно типу - ТТ либо ТТБ либо ЕЭБ
        public Result<Tttb> GetMAIN_TT(string name_dse, string type_work,short tip)
        {
            return GetTIPTreeTP<Tttb>(name_dse, type_work, tip, deflevel, defid_operation, defnumber_trek, defpornom, defattribute_single, defattribute_doc, defid_table);
        }
        /// <summary>
        /// Свободный текст ко всему ТП- главный раздел - не содержит подразделы
        /// </summary>
        [Route("freetext/get")]//Свободный текст ко всему ТП
        public Result<FreeText> GetMAIN_FREETEXT(string name_dse, string type_work) 
        {
            return GetTIPTreeTP<FreeText>(name_dse, type_work, (int)TypeChapter.MAIN_FREETEXT, deflevel, defid_operation, defnumber_trek, defpornom, defattribute_single, defattribute_doc, defid_table);
        }
        /// <summary>
        /// Общие данные  - подраздел дерева - содержится в главном разделе дерева ТТП MAIN_DATA
        /// </summary>
        [Route("data/get")]//Общие данные
        public ResultClass<TotalInformation> GetDATA(string name_dse, string type_work) 
        {
            Result<TotalInformation> result = GetTIPTreeTP<TotalInformation>(name_dse, type_work, (short)TypeChapter.DATA, deflevel, defid_operation, defnumber_trek, defpornom, defattribute_single, defattribute_doc, defid_table); ;
            return ConvertResultToResultClass(result);
        }
        /// <summary>
        /// подписи  - подраздел дерева - содержится в главном разделе дерева ТТП MAIN_DATA
        /// </summary>
        [Route("signs_avtor/get")]//возвращает атора тп или извещения зависит от attribute_doc
        public ResultClass<Avtor> GetSIGNS_avtor(string name_dse, string type_work, short attribute_doc)
        {
             return GetAvtor(name_dse, type_work, attribute_doc);
        }
        [Route("signs_executor/get")]//возвращает соисполнителей ТП или извещения зависит от attribute_doc
        public Result<Soisp> GetSIGNS_executor(string name_dse, string type_work, short attribute_doc)
        {
            return GetTIPTreeTP<Soisp>(name_dse, type_work, (short)TypeChapter.SIGNS, deflevel, defid_operation, defnumber_trek, defpornom, 1, attribute_doc, defid_table);
        }
        [Route("signs_coordinating/get")]//возвращает согласующих тп или извещения зависит от attribute_doc
        public Result<Coordinating> GetSIGNS_coordinating(string name_dse, string type_work, short attribute_doc) 
        {
            return GetCoordinating(name_dse, type_work, attribute_doc);
        }
        /// <summary>
        /// исполнения - подраздел дерева - содержится в главном разделе дерева ТТП MAIN_DATA
        /// </summary>
        [Route("perfomances/get")]//Исполнения
        public Result<Boddse> GetPERFOMANCES(string name_dse, string type_work) 
        {
            return GetTIPTreeTP<Boddse>(name_dse, type_work, (short)TypeChapter.PERFOMANCES, deflevel, defid_operation, defnumber_trek, defpornom, defattribute_single, defattribute_doc, defid_table);
        }
        /// <summary>
        /// операция [А] -конкретная операция - содержится в главном разделе дерева ТТП MAIN_OPERATIONS
        /// </summary>
        [Route("operation/get")]//Операции
        public ResultClass<Operation> GetOPERATION(string name_dse, string type_work, Guid id_operation) 
        {
            Result<Operation> result = GetTIPTreeTP<Operation>(name_dse, type_work, (short)TypeChapter.OPERATION, deflevel, id_operation, defnumber_trek, defpornom, defattribute_single, defattribute_doc, defid_table);
            return ConvertResultToResultClass(result);
        }
        /// <summary>
        /// эскиз  -конкретный эскиз - содержится в главном разделе дерева ТТП MAIN_IMAGES, в конкретной операции OPERATION или в переходе TREK
        /// </summary>
        [Route("image/get")]//Эскиз от уровня зависит к переходам , к операциям или ко всему тп
        public ResultClass<Image> GetIMAGE(string name_dse, string type_work, short level, Guid id_operation,string number_trek,  short pornom) 
        {
            Result<Image> result = GetTIPTreeTP<Image>(name_dse, type_work, (short)TypeChapter.IMAGE, level, id_operation, number_trek, pornom, defattribute_single, defattribute_doc, defid_table);
            return ConvertResultToResultClass(result);
        }
        /// <summary>
        /// Оборудование [Б] -конкретное оборудование - содержится в разделе оборудований EQUIPMENTS
        /// </summary>
        [Route("equipment/get")]//Оборудование
        public ResultClass<Equipment> GetEQUIPMENT(string name_dse, string type_work, Guid id_operation, short pornom) 
        {
            Result<Equipment> result = GetTIPTreeTP<Equipment>(name_dse, type_work, (short)TypeChapter.EQUIPMENT, deflevel,  id_operation, defnumber_trek, pornom, defattribute_single, defattribute_doc, defid_table);
            return ConvertResultToResultClass(result);
        }
        /// <summary>
        /// Тех деталь [К]- конкретная тех деталь - содержится в разделе TECHNICAL_DETAILS
        /// </summary>
        [Route("technical_detail/get")]//Технологические детали
        public ResultClass<TechnicalDetails> GetTECHNICAL_DETAIL(string name_dse, string type_work,  Guid id_operation, short pornom) 
        {
            Result<TechnicalDetails> result = GetTIPTreeTP<TechnicalDetails>(name_dse, type_work, (short)TypeChapter.TECHNICAL_DETAIL, deflevel,  id_operation,defnumber_trek, pornom, defattribute_single, defattribute_doc, defid_table);
            return ConvertResultToResultClass(result);
        }
        /// <summary>
        /// Материал [М]- конкретный материал - содержится в разделе MATERIALS
        /// </summary>
        [Route("material/get")]//Материалы
        public ResultClass<Material> GetMATERIAL(string name_dse, string type_work,   Guid id_operation, short pornom) 
        {
            Result<Material> result = GetTIPTreeTP<Material>(name_dse, type_work, (short)TypeChapter.MATERIAL, deflevel,  id_operation, defnumber_trek, pornom, defattribute_single, defattribute_doc, defid_table);
            return ConvertResultToResultClass(result);
        }
        /// <summary>
        /// Оснастка [Т]- конкретная оснастка - содержится в разделе RIGGINGS 
        /// </summary>
        [Route("rigging/get")]//Оснастка от уровня зависит к переходам или к операциям
        public ResultClass<Rigging> GetRIGGING(string name_dse, string type_work,  short level, Guid id_operation, string number_trek, short pornom) 
        {
            Result<Rigging> result = GetTIPTreeTP<Rigging>(name_dse, type_work, (short)TypeChapter.RIGGING, level,  id_operation, number_trek, pornom , defattribute_single, defattribute_doc, defid_table);
            return ConvertResultToResultClass(result);
        }
        /// <summary>
        /// Режим [Р]- конкретный режим - содержится в разделе MODES
        /// </summary>
        [Route("mode/get")]//Режимы от уровня зависит к переходам или к операциям
        public ResultClass<Mode> GetMODE(string name_dse, string type_work, short level, Guid id_operation, string number_trek, short pornom)
        {
            Result<Mode> result = GetTIPTreeTP<Mode>(name_dse, type_work, (short)TypeChapter.MODE, level,  id_operation, number_trek, pornom, defattribute_single, defattribute_doc, defid_table);
            return ConvertResultToResultClass(result);
        }
        /// <summary>
        /// Переход [O] -  конкретный переход - содержится в разделе TREKS
        /// </summary>
        [Route("trek/get")]//Переходы
        public ResultClass<Trek> GetTREK(string name_dse, string type_work,  Guid id_operation, string number_trek ) 
        {
            Result<Trek> result = GetTIPTreeTP<Trek>(name_dse, type_work, (short)TypeChapter.TREK, deflevel,  id_operation, number_trek, defpornom, defattribute_single, defattribute_doc, defid_table);
            return ConvertResultToResultClass(result);
        }
        /// <summary>
        /// Внимание [B] (конкретная запись Внимания)  - содержится в разделе ATTENTION
        /// </summary>
        [Route("attention_record/get")]//Свободный  текст к операции (ВНИМАНИЕ [В] ) 
        public ResultClass<FreeText> GetATTENTION_RECORD(string name_dse, string type_work, Guid id_operation,  short pornom)
        {
            Result<FreeText> result = GetTIPTreeTP<FreeText>(name_dse, type_work, (short)TypeChapter.ATTENTION_RECORD, deflevel,  id_operation, defnumber_trek, pornom, defattribute_single, defattribute_doc, defid_table);
            return ConvertResultToResultClass(result);
        }
        /// <summary>
        /// Указание [У] (конкретная запись Указания)  - содержится в разделе DESIGNATION
        /// </summary>
        [Route("designation_record/get")]//Свободный  текст (УКАЗАНИЕ [У]) от уровня зависит к переходам или к операциям
        public ResultClass<FreeText> GetDESIGNATION_RECORD(string name_dse, string type_work, short level, Guid id_operation, string number_trek, short pornom)  
        {
            Result<FreeText> result = GetTIPTreeTP<FreeText>(name_dse, type_work, (short)TypeChapter.DESIGNATION_RECORD, level, id_operation, number_trek, pornom, defattribute_single, defattribute_doc, defid_table);
            return ConvertResultToResultClass(result);
        }
        /// <summary>
        /// Лист изменений (только для корректировки) - главный раздел  - не содержит подразделы
        /// </summary>
        [Route("main_change_sheet/get")]//ЛИСТ изменений
        public ResultClass<MainChangeSheet> GetMAIN_CHANGE_SHEET(string name_dse, string type_work) 
        {
            Result<MainChangeSheet> result = GetTIPTreeTP<MainChangeSheet>(name_dse, type_work, (short)TypeChapter.MAIN_CHANGE_SHEET, deflevel, defid_operation, defnumber_trek, defpornom, defattribute_single, defattribute_doc, defid_table);
            return ConvertResultToResultClass(result);
        }
        [Route("table_ktp/get")]//ТАБЛИЦЫ [КТП] или ТАБЛИЦЫ [ВТП](зависит от типа)
        public Result<TableTP> GetTABLEKTP(short Tip, Guid id_table) 
        {
           return GetTIPTreeTP<TableTP>("", "", Tip, deflevel, defid_operation, defnumber_trek, defpornom, defattribute_single, defattribute_doc, id_table);
        }
        #endregion


        [Route("list_method_transaction/get")]
        public Result<ListMethodTransaction> GetListMethodTransaction()
        {
            return GetListMethodTransaction<ListMethodTransaction>((short)TypeArrays.LIST_METHOD_TRANSACTION);
        }
        #endregion


        #endregion

        #region POST
        /// <summary>
        ///мето получает всё дерево ТТП и режим его работы
        /// </summary>
        /// <param name="information"></param>
        /// <param name="correction"></param>
        /// <returns></returns>
        [Route("add_operation/Set")]
        [HttpPost]
        public string SetOperation([FromBody] TTP information, bool correction)//true-коррекция иначе проектирование
        {
            try
            {
                if(information.operation!=null)
                    tPPService.StoreChangeOperation(information.operation, correction);
                if (information.freeText != null)
                    tPPService.StoreChangeFreeText(information.freeText, correction);
                /// <summary>
                /// работает только в режиме коррекции, нельзя создать и удалить
                /// </summary>
                if (information.totalInformation != null)
                    tPPService.StoreTotalInformation(information.totalInformation);
                if (information.permission != null)
                    tPPService.StoreChangePermission(information.permission, correction);

                return "true";
            }
            catch(Exception e)
            {
                return e.Message;
            }
            finally
            {
                tPPService.Dispose();
            }
        }

        #endregion

        #region Add

        [Route("Method_create_ttp/add")]
        public void AddMethodCreatTtp(string name_dse, string type_work, int regim, string fio, string login, string no, string po, int prgrtp) //в начале наименования метода должно стоять Get 
        {
            AddMethodCreatTtpOrNotice(name_dse, type_work, regim, fio, login, no, po, prgrtp);
        }
        #endregion


        #region private
        

        private ResultClass<Avtor> GetAvtor(string name_dse, string type_work,short attribute_doc)
        {
            Avtor result = null;
            
                if (attribute_doc == 1)
                {
                    Result<Tlp> results = GetTIPTreeTP<Tlp>(name_dse, type_work, (short)TypeChapter.SIGNS, deflevel, defid_operation, defnumber_trek, defpornom, 0, attribute_doc, defid_table);
                    if(results.ErrorMessage != null)
                        return new ResultClass<Avtor>(results.ErrorMessage);
                    Tlp tlp = results.Results.FirstOrDefault();
                    if (tlp != null)
                        result = new Avtor(tlp.Avtor, tlp.DpAvtor);
                }
                else
                {                
                    Result<MainChangeSheet> results = GetTIPTreeTP<MainChangeSheet>(name_dse, type_work, (short)TypeChapter.SIGNS, deflevel, defid_operation, defnumber_trek, defpornom, 0, attribute_doc, defid_table);
                    if(results.ErrorMessage != null)
                        return new ResultClass<Avtor>(results.ErrorMessage);
                    MainChangeSheet operationp = results.Results.FirstOrDefault();
                    if (operationp!= null)
                        result = new Avtor(operationp.AVTOR, operationp.DPAVTOR);
                }
            
            return new ResultClass<Avtor>(result);
        }
        private Result<Coordinating> GetCoordinating(string name_dse, string type_work, short attribute_doc)
        {
            List<Coordinating> result = null;
            if (attribute_doc == 1)
            {
                Result<Sogltp> sogltp = GetTIPTreeTP<Sogltp>(name_dse, type_work, (short)TypeChapter.SIGNS, deflevel, defid_operation, defnumber_trek, defpornom, 2, 1, defid_table);
                if(sogltp.ErrorMessage != null)
                    return new Result<Coordinating>(sogltp.ErrorMessage);
                result = sogltp.Results.Select(x => new Coordinating(x.fiosogl, x.dpsogl, x.nazsogl)).ToList();
            }
            else
            {
                Result<Soglizv> sogliz = GetTIPTreeTP<Soglizv>(name_dse, type_work, (short)TypeChapter.SIGNS, deflevel, defid_operation, defnumber_trek, defpornom, 2, 0, defid_table);
                if (sogliz.ErrorMessage !=null)
                    return new Result<Coordinating>(sogliz.ErrorMessage);
                result = sogliz.Results.Select(x => new Coordinating(x.fiosogl, x.dpsogl, x.nazsogl)).ToList();                
            }
            return new Result<Coordinating>(result);
        }
        private Result<TTPForRead> GetTreeTtp(string name_dse, string type_work)
        {
            try
            {
                return new Result<TTPForRead>(tPPService.GetTreeTP(name_dse, type_work));                
            }
            catch (Exception e)
            {
                return  new Result<TTPForRead>(e.Message);
            }
            finally
            {
                tPPService.Dispose();
            }

        }
        private Result<T> GetTIPTreeTP<T>(string name_dse, string type_work, short tip, short level, Guid id_operation, string number_trek, short pornom, short attribute_single, short attribute_doc, Guid id_table)
            where T:AbstractSkat
        {

            name_dse = HttpUtility.UrlDecode(name_dse);
            number_trek = HttpUtility.UrlDecode(number_trek);
            if (number_trek == null) { number_trek = ""; }
            //проверка на правильность параметров
            if (name_dse.Trim() == "" || (type_work != "60")) //60-вид работы ТТП
                throw new Exception("Переданы неправильные параметры");
            try
            {
                return new Result<T>(tPPService.GetTIPTreeTP<T>(name_dse, type_work, tip, level, id_operation, number_trek, pornom, attribute_single, attribute_doc, id_table));

            }
            catch (Exception e)
            {
                return new Result<T>(e.Message);
            }
            finally
            {
                tPPService.Dispose();
            }

        }
        private Result<T> GetTipTechnicalArrays<T>(int nom_mas, int tip,string cex)
        {
            //проверка на правильность параметров
            if (nom_mas <= 0)
            {
                return null;
            }
            try
            { 
                return new Result<T>(pctextService.GetTipTechnicalArrays<T>(nom_mas, tip, cex));
            }
            catch (Exception e)
            {
                return new Result<T>(e.GetBaseException().Message);
            }
            finally
            {
                tPPService.Dispose();
            }
        }
        private ResultClass<T> ConvertResultToResultClass<T>(Result<T> result)
        {
            if (result.ErrorMessage!=null)
                return new ResultClass<T>(result.ErrorMessage);
            else
                return new ResultClass<T>(result.Results.FirstOrDefault());
        }
        private Result<TreeShablone> GetTree(int nom_mas)
        {
            //проверка на правильность параметров
            if (nom_mas <= 0)
                return null;
            try
            {
                return new Result<TreeShablone>(pctextService.GetTree(nom_mas));
            }
            catch (Exception e)
            {
                return new Result<TreeShablone>(e.GetBaseException().Message);
            }
            finally
            {
                tPPService.Dispose();
            }
            
        }
        private ResultClass<AllOperationInformation> GetAllShabloneOperation(int nom_mas, Guid id_operation,  int pornom)
        {
            //проверка на правильность параметров
            if (nom_mas <= 0)
                return null;
            try
            {
                return new ResultClass<AllOperationInformation>( (AllOperationInformation)pctextService.GetTipShabloneOperation((int)nom_mas, (Guid)id_operation, (int)pornom));
            }
            catch (Exception e)
            {
                return new ResultClass<AllOperationInformation>(e.GetBaseException().Message);
            }
            finally
            {
                tPPService.Dispose();
            }
            
        }
        private ResultClass<AllTrekInformation> GetForTipShabloneTransition(int nom_mas, Guid id_operation, string number_trek, int pornom)
        {
            //проверка на правильность параметров
            if (nom_mas <= 0)
                return null;
            try
            {
                return new ResultClass<AllTrekInformation>(pctextService.GetForTipShabloneTransition(nom_mas, id_operation, number_trek, pornom));
            }
            catch (Exception e)
            {
                return new ResultClass<AllTrekInformation>(e.GetBaseException().Message);
            }
            finally
            {
                tPPService.Dispose();
            }
            
        }
        private ResultClass<AllTrekInformation> GetAllShabloneTransition(int nom_mas, Guid id_number_trek, int pornom)
        {
            //проверка на правильность параметров
            if (nom_mas <= 0)
                return null;
            try
            {
                return new ResultClass<AllTrekInformation>(pctextService.GetAllShabloneTransition(nom_mas, id_number_trek, pornom));
            }
            catch (Exception e)
            {
                return new ResultClass<AllTrekInformation>(e.GetBaseException().Message);
            }
        }
        
        private Result<T> GetListMethodTransaction<T>(short nom_mas)
        {
            try
            {
                return new Result<T>(tPPService.GetListMethodTransaction<T>(nom_mas));
            }
            catch (Exception e)
            {
                return new Result<T>(e.GetBaseException().Message);
            }
            finally
            {
                tPPService.Dispose();
            }
            
        }


        private void AddMethodCreatTtpOrNotice(string name_dse, string type_work, int regim, string fio, string login, string no, string po, int prgrtp)
        {

            name_dse = HttpUtility.UrlDecode(name_dse);
            fio = HttpUtility.UrlDecode(fio);
            login = HttpUtility.UrlDecode(login);
            no = HttpUtility.UrlDecode(no);
            po = HttpUtility.UrlDecode(po);

            if (name_dse.Trim() == "" ) 
                return;
            try
            {
                tPPService.AddMethodCreatTtpOrNotice(name_dse, type_work, regim, fio, login, no, po, prgrtp);
            }
            catch (Exception e)
            {
                throw new Exception(e.GetBaseException().Message);
            }
            finally
            {
                tPPService.Dispose();
            }
        }
        #endregion
    }



}
