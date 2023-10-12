using E012.DomainModelServer.Model.Entities.PCTEXT;
using E012.DomainModelServer.Model.Entities.PCTEXT.Shablone;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E012.SAPRTKCoating.EFDataSource.DBContext
{
    public class LocalContextPCTEXT: DbContext
    {
        public LocalContextPCTEXT()
            //: base("data source=dbsrvtest;initial catalog=SKAT;user id=as_skat;password=as_te_skat;MultipleActiveResultSets=True;App=EntityFramework")
            : base("name=PCTEXTContext") //тут меняется имя коннекта к базе из app.config, если нужно.
        {
            // Database.CommandTimeout = 180;
            // Database.SetInitializer(new LocalInitializer());
        }
        /// <summary>
        /// Процедура получения дерева типовых операций по номеру массива
        /// </summary>
        /// <param name="nom_mas">Номер массива</param>
        /// <param name="operation">операция или переход</param>
        /// <returns></returns>       
        public IEnumerable<TreeShablonOperationOrTransit> GetTreeOperation(int  nom_mas)
        {
            System.Data.SqlClient.SqlParameter Nom_mas = new System.Data.SqlClient.SqlParameter("@NOM_MAS", nom_mas);
            return this.Database.SqlQuery<TreeShablonOperationOrTransit>("[dbo].[e012p144_TreeStandartOperation]  @NOM_MAS",
                 Nom_mas);
        }
        public IEnumerable<TreeShablonOperationOrTransit> GetTreeTransit(int nom_mas)
        {
            System.Data.SqlClient.SqlParameter Nom_mas = new System.Data.SqlClient.SqlParameter("@NOM_MAS", nom_mas);
            return this.Database.SqlQuery<TreeShablonOperationOrTransit>("[dbo].[e012p143_TreeStandartTransition]  @NOM_MAS",
                 Nom_mas);
        }

        /// <summary>
        /// Процедура получения списков технологическийх массивов по типу
        /// </summary>
        /// <param name="nom_mas">Номер массива</param>
        /// <param name="Tip">тип </param>
        /// <returns></returns>       
        public IEnumerable<T> GetTipTechnicalArrays<T>(int nom_mas, int tip,string cex)
        {
            System.Data.SqlClient.SqlParameter Nom_mas = new System.Data.SqlClient.SqlParameter("@NOM_MAS", nom_mas);
            System.Data.SqlClient.SqlParameter Tip = new System.Data.SqlClient.SqlParameter("@TIP", tip);
            System.Data.SqlClient.SqlParameter Cex = new System.Data.SqlClient.SqlParameter("@cex", cex);
            return this.Database.SqlQuery<T>("[dbo].[e012p145_GetTipTechnicalArrays]  @NOM_MAS, @TIP, @cex",
                 Nom_mas, Tip, Cex);
        }
        /// <summary>
        /// Процедура получение всех операций по номеру массива
        /// </summary>
        // --@nom_mas  - номер массива
        /// <returns></returns>   
        public IEnumerable<OperationShablone> GetAllOperation(int nom_mas)
        {
            System.Data.SqlClient.SqlParameter Nom_mas = new System.Data.SqlClient.SqlParameter("@NOM_MAS", nom_mas);

            return this.Database.SqlQuery<OperationShablone>("SELECT * FROM  operation WHERE NOM_MAS = @NOM_MAS",
                 Nom_mas);
        }

        /// <summary>
        /// Процедура получение операций из дерева стандартных ТТП по типу
        /// </summary>
        // --@nom_mas  - номер массива,
        //--@Level – уровень пункта дерева
        //--@Tip – тип переменной(пункт дерева)
        //--@id_operationeration – идентификатор операции 
        //--@number_trek – номер перехода(0- все, иначе конкретный)
        //--@pornom – порядковый номер(0- все, иначе конкретный)
        /// <returns></returns>   
        public IEnumerable<T> GetTipShabloneOperation<T>(int nom_mas, int level, int tip,Guid id_operationeration,string number_trek,int pornom)
        {
            System.Data.SqlClient.SqlParameter Nom_mas = new System.Data.SqlClient.SqlParameter("@NOM_MAS", nom_mas);
            System.Data.SqlClient.SqlParameter Level = new System.Data.SqlClient.SqlParameter("@LEVEL", level);
            System.Data.SqlClient.SqlParameter Tip = new System.Data.SqlClient.SqlParameter("@TIP", tip);
            System.Data.SqlClient.SqlParameter id_op = new System.Data.SqlClient.SqlParameter("@id_operationeration", id_operationeration);
            System.Data.SqlClient.SqlParameter Number_trek = new System.Data.SqlClient.SqlParameter("@number_trek", number_trek);
            System.Data.SqlClient.SqlParameter Pornom = new System.Data.SqlClient.SqlParameter("@PORNOM", pornom);

            return this.Database.SqlQuery<T>("[dbo].[e012p147_GetTipStandartOperation]  @NOM_MAS, @LEVEL,@TIP, @id_operationeration,@number_trek, @PORNOM",
                 Nom_mas, Level, Tip, id_op, Number_trek, Pornom);
        }
        /// <summary>
        /// Процедура получение операций из дерева стандартных ТТП по типу
        /// </summary>
        // --@nom_mas  - номер массива,
        //--@Level – уровень пункта дерева
        //--@Tip – тип переменной(пункт дерева)
        //--@id_operationeration – идентификатор операции 
        //--@number_trek – номер перехода(0- все, иначе конкретный)
        //--@pornom – порядковый номер(0- все, иначе конкретный)
        /// <returns></returns>   
        public IEnumerable<T> GetTipShabloneTransition<T>(int nom_mas,  int tip, Guid id_number_trek,  int pornom)
        {
            System.Data.SqlClient.SqlParameter Nom_mas = new System.Data.SqlClient.SqlParameter("@NOM_MAS", nom_mas);
            System.Data.SqlClient.SqlParameter Tip = new System.Data.SqlClient.SqlParameter("@TIP", tip);
            System.Data.SqlClient.SqlParameter Id_number_trek = new System.Data.SqlClient.SqlParameter("@ID_number_trek", id_number_trek);
            System.Data.SqlClient.SqlParameter Pornom = new System.Data.SqlClient.SqlParameter("@PORNOM", pornom);

            return this.Database.SqlQuery<T>("[dbo].[e012p146_GetTipStandartTransition]  @NOM_MAS, @TIP, @id_operationeration, @PORNOM",
                 Nom_mas, Tip, Id_number_trek, Pornom);
        }

        /// <summary>
        /// Процедура получения списка технических массивов
        /// </summary>
        /// <returns></returns>       
        public IEnumerable<TechnicalArrays> GetTechnicalArrays()
        {
            return this.Database.SqlQuery<TechnicalArrays>("SELECT MAS_NAIM,NAME_MAS,po FROM  Massives order by NAME_MAS");
        }
    }
}
