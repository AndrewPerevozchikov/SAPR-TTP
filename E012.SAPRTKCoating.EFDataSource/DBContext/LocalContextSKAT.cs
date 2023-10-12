using System.Collections.Generic;
using System.Data.Entity;
using System.Configuration;
using System;
using E012.DomainModelServer.Model.Entities.SKAT;

namespace E012.SAPRTKCoating.EFDataSource.DBContext
{
    public class LocalContextSKAT : DbContext
    {
        public LocalContextSKAT()
            //: base("data source=dbsrvtest;initial catalog=SKAT;user id=as_skat;password=as_te_skat;MultipleActiveResultSets=True;App=EntityFramework")
            : base("name=SKATContext") //тут меняется имя коннекта к базе из app.config, если нужно.
        {
            // Database.CommandTimeout = 180;
            // Database.SetInitializer(new LocalInitializer());
        }

        /// <summary>
        /// Процедура получения дерева ТТП по дсе и виду технологий
        /// </summary>
        /// <param name="name_dse">ДСЕ</param>
        /// <param name="type_work">вид технологии</param>
        /// <returns></returns>       
        public IEnumerable<TTPForRead> GetTreeTP( string name_dse, string type_work)
        {
            System.Data.SqlClient.SqlParameter Name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", name_dse);
            System.Data.SqlClient.SqlParameter Type_work = new System.Data.SqlClient.SqlParameter("@type_work", type_work);

            return this.Database.SqlQuery<TTPForRead>("[dbo].[e012p141_treeTTP]  @name_dse, @type_work",
                 Name_dse, Type_work);
        }
        public IEnumerable<T> GetTIPTreeTP<T>(string name_dse, string type_work,int tip,int level, Guid id_operation, string number_trek,int pornom,int attribute_single,int attribute_doc, Guid id_table)
        {
            System.Data.SqlClient.SqlParameter Name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", name_dse);
            System.Data.SqlClient.SqlParameter Type_work = new System.Data.SqlClient.SqlParameter("@type_work", type_work);
            System.Data.SqlClient.SqlParameter TIP = new System.Data.SqlClient.SqlParameter("@TIP", tip);
            System.Data.SqlClient.SqlParameter Level = new System.Data.SqlClient.SqlParameter("@Level", level);
            System.Data.SqlClient.SqlParameter id_op= new System.Data.SqlClient.SqlParameter("@id_operation", id_operation);
            System.Data.SqlClient.SqlParameter Number_trek = new System.Data.SqlClient.SqlParameter("@number_trek", number_trek);
            System.Data.SqlClient.SqlParameter PORNOM = new System.Data.SqlClient.SqlParameter("@PORNOM", pornom);
            System.Data.SqlClient.SqlParameter ATTRIBUTE_SINDL = new System.Data.SqlClient.SqlParameter("@ATTRIBUTE_SINDL", attribute_single);
            System.Data.SqlClient.SqlParameter ATTRIBUTE_DOC = new System.Data.SqlClient.SqlParameter("@ATTRIBUTE_DOC", attribute_doc);
            System.Data.SqlClient.SqlParameter ID_TABLE = new System.Data.SqlClient.SqlParameter("@ID_TABLE", id_table);

            return this.Database.SqlQuery<T>("[dbo].[e012p142_etTipTreeTTP]  @name_dse, @type_work, @TIP, @Level, @id_operation, @number_trek, @PORNOM, @ATTRIBUTE_SINDL, @ATTRIBUTE_DOC,@ID_TABLE",
                 name_dse, type_work, TIP, Level, id_op, Number_trek, PORNOM, ATTRIBUTE_SINDL, ATTRIBUTE_DOC, ID_TABLE);
        }
        public IEnumerable<T> GetListMethodTransaction<T>(int type)
        {

            System.Data.SqlClient.SqlParameter Nom_mas = new System.Data.SqlClient.SqlParameter("@Type", type);

            return this.Database.SqlQuery<T>("[dbo].[e012p143_GetDataFromDirectories]  @Type",
                 Nom_mas);
        }

        public void StoreData(Operation operation,string newversion)
        {
            System.Data.SqlClient.SqlParameter id_operation = new System.Data.SqlClient.SqlParameter("@id_operation", operation.id_operation);
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", operation.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", operation.type_work);
            System.Data.SqlClient.SqlParameter number_operation = new System.Data.SqlClient.SqlParameter("@number_operation", operation.number_operation);
            System.Data.SqlClient.SqlParameter version = new System.Data.SqlClient.SqlParameter("@version", newversion);
            System.Data.SqlClient.SqlParameter nc = new System.Data.SqlClient.SqlParameter("@nc", operation.NC);
            System.Data.SqlClient.SqlParameter kod_oper = new System.Data.SqlClient.SqlParameter("@kod_oper", operation.kod_oper);
            System.Data.SqlClient.SqlParameter nmopr = new System.Data.SqlClient.SqlParameter("@nmopr", operation.nmopr);
            System.Data.SqlClient.SqlParameter obdok = new System.Data.SqlClient.SqlParameter("@obdok", operation.OBDOK);
            System.Data.SqlClient.SqlParameter niot = new System.Data.SqlClient.SqlParameter("@niot", operation.niot);
            System.Data.SqlClient.SqlParameter vid_pr = new System.Data.SqlClient.SqlParameter("@vid_pr", operation.vid_pr);
            System.Data.SqlClient.SqlParameter number_operationti = new System.Data.SqlClient.SqlParameter("@number_operationti", operation.number_operationti);
            System.Data.SqlClient.SqlParameter ki = new System.Data.SqlClient.SqlParameter("@ki", operation.ki);
            System.Data.SqlClient.SqlParameter number_operationst_st = new System.Data.SqlClient.SqlParameter("@number_operationst_st", operation.number_operationst_st);
            
            if ((short)operation.typeState == 1)//добавление
            {
                this.Database.ExecuteSqlCommand("insert into operation (name_dse,type_work,number_operation,version,nc,kod_oper,nmopr,obdok,niot,vid_pr,number_operationti,ki,number_operationst_st,id_operation) values " +
                    "(@name_dse,@type_work,@number_operation,@version,@nc,@kod_oper,@nmopr,@obdok,@niot,@vid_pr,@number_operationti,2,0,@id_operation)",
                    name_dse, type_work, number_operation, version, nc, kod_oper, nmopr, obdok, niot, vid_pr, number_operationti, ki, number_operationst_st, id_operation);
            }
            if ((short)operation.typeState == 2)//изменение
            {
                this.Database.ExecuteSqlCommand("update operation set version=@version,nc=@nc,kod_oper=@kod_oper,nmopr=@nmopr,obdok=@obdok,niot=@niot,vid_pr=@vid_pr,number_operationti=@number_operationti,number_operation=@number_operation where  id_operation=@id_operation"
                ,id_operation, number_operation, version, nc, kod_oper, nmopr, obdok, niot, vid_pr, number_operationti);
                string[] tabels = { "trek", "attention", "operationft", "bdolog",  "eqipment", "details", "material", "mode", "properti", "image" };
                foreach(var item in tabels)
                {
                    this.Database.ExecuteSqlCommand("update "+ item+ " set number_operation=@number_operation where id_operation=@id_operation", number_operation, id_operation);
                }
                this.Database.ExecuteSqlCommand("update bdkomm set yk=@number_operation where name_dse=@name_dse AND type_work = @type_work AND yk=@number_operation and priz = 0", number_operation, name_dse,type_work);
            }
            if ((short)operation.typeState == 3)//удаление
            {   //потом переделать хранимую!!!
                //this.Database.ExecuteSqlCommand("[ ydaloper]");
            }
        }
        public void StoreData(Trek trek, string newversion)
        {
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", trek.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", trek.type_work);
            System.Data.SqlClient.SqlParameter number_operation = new System.Data.SqlClient.SqlParameter("@number_operation", trek.number_operation);
            System.Data.SqlClient.SqlParameter version = new System.Data.SqlClient.SqlParameter("@version", newversion);
            System.Data.SqlClient.SqlParameter number_trek = new System.Data.SqlClient.SqlParameter("@number_trek", trek.number_trek);
            System.Data.SqlClient.SqlParameter information = new System.Data.SqlClient.SqlParameter("@information", trek.information);
            System.Data.SqlClient.SqlParameter number_operationst_st = new System.Data.SqlClient.SqlParameter("@number_operationst_st", trek.number_operationst_st);
            System.Data.SqlClient.SqlParameter id_number_trek = new System.Data.SqlClient.SqlParameter("@id_number_trek", trek.id_number_trek);
            System.Data.SqlClient.SqlParameter id_operation = new System.Data.SqlClient.SqlParameter("@id_operation", trek.id_operation);
            System.Data.SqlClient.SqlParameter work_time = new System.Data.SqlClient.SqlParameter("@work_time", trek.number_operation);

            if ((short)trek.typeState == 1)//добавление
            {
                this.Database.ExecuteSqlCommand("insert into trek (name_dse,type_work,number_operation,version,number_trek,information,number_operationst_st,id_number_trek,id_operation,work_time)  values " +
                    "(@name_dse,@type_work,@number_operation,@version,@number_trek,@information,0,newid(),@id_operation,@work_time)", 
                    name_dse, type_work, number_operation, version, number_trek, information, id_operation, work_time);
            }
            if ((short)trek.typeState == 2)//изменение
            {
                this.Database.ExecuteSqlCommand("update operation set version=@version,number_trek=@number_trek,information=@information where id_number_trek=@id_number_trek"
                , name_dse, type_work, number_operation, version,id_number_trek);
                string[] tabels = { "attention", "mode", "properti", "image" };
                foreach (var item in tabels)
                {
                    this.Database.ExecuteSqlCommand("update " + item + " set number_trek=@number_trek where name_dse=@name_dse and type_work=@type_work and id_number_trek=@id_number_trek", number_trek, id_number_trek);
                }
            }
            if ((short)trek.typeState == 3)//удаление
            {
                string[] tabels = { "trek","attention", "mode", "properti", "image" };
                foreach (var item in tabels)
                {
                    this.Database.ExecuteSqlCommand("DELETE from " + item + " where id_number_trek=@id_number_trek", id_number_trek);
                }
            }
        }
        public void StoreData(Equipment equipment, string newversion)
        {
            System.Data.SqlClient.SqlParameter id_operation = new System.Data.SqlClient.SqlParameter("@id_operation", equipment.id_operation);
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", equipment.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", equipment.type_work);
            System.Data.SqlClient.SqlParameter number_operation = new System.Data.SqlClient.SqlParameter("@number_operation", equipment.number_operation);
            System.Data.SqlClient.SqlParameter version = new System.Data.SqlClient.SqlParameter("@version", newversion);
            System.Data.SqlClient.SqlParameter pornom = new System.Data.SqlClient.SqlParameter("@pornom", equipment.pornom);
            System.Data.SqlClient.SqlParameter kod = new System.Data.SqlClient.SqlParameter("@kod", equipment.kod);
            System.Data.SqlClient.SqlParameter nst = new System.Data.SqlClient.SqlParameter("@nst", equipment.NST);
            System.Data.SqlClient.SqlParameter nn = new System.Data.SqlClient.SqlParameter("@nn", equipment.nn);
            System.Data.SqlClient.SqlParameter kprof = new System.Data.SqlClient.SqlParameter("@kprof", equipment.kprof);
            System.Data.SqlClient.SqlParameter ki = new System.Data.SqlClient.SqlParameter("@ki", equipment.ki);
            System.Data.SqlClient.SqlParameter koid = new System.Data.SqlClient.SqlParameter("@koid", equipment.koid);
            System.Data.SqlClient.SqlParameter number_operationst_st = new System.Data.SqlClient.SqlParameter("@number_operationst_st", equipment.number_operationst_st);
            System.Data.SqlClient.SqlParameter id_recno = new System.Data.SqlClient.SqlParameter("@id_recno", equipment.id_recno);

            if ((short)equipment.typeState == 1)//добавление
            {
                this.Database.ExecuteSqlCommand("insert into eqipment  (name_dse,type_work,number_operation,version,pornom,kod,nst,nn,kprof,ki,koid,number_operationst_st,id_recno,id_operation) values " +
                    "(@name_dse,@type_work,@number_operation,@version,@pornom,@kod,@nst,@nn,@kprof,@ki,@koid,0,null,@id_operation)",
                    name_dse, type_work, number_operation, version, pornom, kod, nst, nn, kprof, ki, koid, id_operation);
            }
            if ((short)equipment.typeState == 2)//изменение
            {
                this.Database.ExecuteSqlCommand("update eqipment set version=@version,kod=@kod,nst=@nst,nn=@nn,kprof=@kprof,ki=@ki,koid=@koid where id_operation=@id_operation AND pornom=@pornom"
                , version, pornom, kod, nst, nn, kprof, ki, koid, id_operation);
            }
            if ((short)equipment.typeState == 3)//удаление
            {
                this.Database.ExecuteSqlCommand("DELETE from eqipment where id_operation=@id_operation AND pornom=@pornom", id_operation, pornom);
            }
        }
       
        public void StoreData(Material material, string newversion)
        {
            System.Data.SqlClient.SqlParameter id_operation = new System.Data.SqlClient.SqlParameter("@id_operation", material.id_operation);
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", material.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", material.type_work);
            System.Data.SqlClient.SqlParameter number_operation = new System.Data.SqlClient.SqlParameter("@number_operation", material.number_operation);
            System.Data.SqlClient.SqlParameter version = new System.Data.SqlClient.SqlParameter("@version", newversion);
            System.Data.SqlClient.SqlParameter pornom = new System.Data.SqlClient.SqlParameter("@pornom", material.pornom);
            System.Data.SqlClient.SqlParameter naimc = new System.Data.SqlClient.SqlParameter("@naimc", material.naimc);
            System.Data.SqlClient.SqlParameter ei = new System.Data.SqlClient.SqlParameter("@ei", material.ei);
            System.Data.SqlClient.SqlParameter vr = new System.Data.SqlClient.SqlParameter("@vr", material.vr);
            System.Data.SqlClient.SqlParameter en = new System.Data.SqlClient.SqlParameter("@en", material.en);
            System.Data.SqlClient.SqlParameter pokr = new System.Data.SqlClient.SqlParameter("@pokr", material.pokr);
            System.Data.SqlClient.SqlParameter krnaimc = new System.Data.SqlClient.SqlParameter("@krnaimc", material.krnaimc);
            System.Data.SqlClient.SqlParameter number_operationst_st = new System.Data.SqlClient.SqlParameter("@number_operationst_st", material.number_operationst_st);
            System.Data.SqlClient.SqlParameter protctp = new System.Data.SqlClient.SqlParameter("@protctp", material.protctp);
            System.Data.SqlClient.SqlParameter datkor = new System.Data.SqlClient.SqlParameter("@datkor", material.datkor);
            System.Data.SqlClient.SqlParameter ki = new System.Data.SqlClient.SqlParameter("@ki", material.ki);
            System.Data.SqlClient.SqlParameter id_nn_material = new System.Data.SqlClient.SqlParameter("@id_nn_material", material.id_nn_material);
            System.Data.SqlClient.SqlParameter handle_input = new System.Data.SqlClient.SqlParameter("@handle_input", material.handle_input);
            System.Data.SqlClient.SqlParameter Concetration = new System.Data.SqlClient.SqlParameter("@Concetration", material.Concetration);
            System.Data.SqlClient.SqlParameter Viscosity = new System.Data.SqlClient.SqlParameter("@Viscosity", material.Viscosity);
            System.Data.SqlClient.SqlParameter Density = new System.Data.SqlClient.SqlParameter("@Density", material.Density);
            System.Data.SqlClient.SqlParameter NN = new System.Data.SqlClient.SqlParameter("@NN", material.NN);
            if ((short)material.typeState == 1)//добавление
            {
                this.Database.ExecuteSqlCommand("insert into material  (name_dse,type_work,number_operation,version,pornom,naimc,ei,vr,en,pokr,krnaimc,number_operationst_st,protctp,datkor,ki,id_operation,id_nn_material,handle_input,Concetration,Viscosity,Density,NN)  values " +
                    "(@name_dse,@type_work,@number_operation,@version,@pornom,@naimc,@ei,@vr,@en,@pokr,@krnaimc,0,@protctp,getdate(),2,@id_operation,@id_nn_material,1,@Concetration,@Viscosity,@Density,@NN)",
                    name_dse, type_work, number_operation, version, pornom, naimc, ei, vr, en, pokr, krnaimc, protctp, id_operation, id_nn_material, Concetration, Viscosity, Density, NN);
            }
            if ((short)material.typeState == 2)//изменение
            {
                this.Database.ExecuteSqlCommand("update material  set version=@version,naimc=@naimc,ei=@ei,vr=@vr,en=@en,pokr=@pokr,krnaimc=@krnaimc,protctp=@protctp,datkor=@datkor,Concetration=@Concetration,Viscosity=@Viscosity,Density=@Density,NN=@NN where id_operation=@id_operation AND pornom=@pornom"
                , version, pornom,  naimc, ei, vr, en, pokr, krnaimc, protctp, datkor, Concetration, Viscosity, Density, NN, id_operation);
            }
            if ((short)material.typeState == 3)//удаление
            {
                this.Database.ExecuteSqlCommand("DELETE from material  where id_operation=@id_operation AND pornom=@pornom", id_operation, pornom);
            }
        }
        public void StoreData(Mode mode, string newversion)
        {
            System.Data.SqlClient.SqlParameter id_operation = new System.Data.SqlClient.SqlParameter("@id_operation", mode.id_operation);
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", mode.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", mode.type_work);
            System.Data.SqlClient.SqlParameter number_operation = new System.Data.SqlClient.SqlParameter("@number_operation", mode.number_operation);
            System.Data.SqlClient.SqlParameter version = new System.Data.SqlClient.SqlParameter("@version", newversion);
            System.Data.SqlClient.SqlParameter number_trek = new System.Data.SqlClient.SqlParameter("@number_trek", mode.number_trek);
            System.Data.SqlClient.SqlParameter pornom = new System.Data.SqlClient.SqlParameter("@pornom", mode.pornom);
            System.Data.SqlClient.SqlParameter sodre = new System.Data.SqlClient.SqlParameter("@sodre", mode.sodre);
            System.Data.SqlClient.SqlParameter number_operationst_st = new System.Data.SqlClient.SqlParameter("@number_operationst_st", mode.number_operationst_st);
            System.Data.SqlClient.SqlParameter P = new System.Data.SqlClient.SqlParameter("@P", mode.P);
            System.Data.SqlClient.SqlParameter t = new System.Data.SqlClient.SqlParameter("@t", mode.t);
            System.Data.SqlClient.SqlParameter pH = new System.Data.SqlClient.SqlParameter("@pH", mode.pH);
            System.Data.SqlClient.SqlParameter OPA_K = new System.Data.SqlClient.SqlParameter("@OPA_K", mode.OPA_K);
            System.Data.SqlClient.SqlParameter D = new System.Data.SqlClient.SqlParameter("@D", mode.D);
            System.Data.SqlClient.SqlParameter U = new System.Data.SqlClient.SqlParameter("@U", mode.U);
            System.Data.SqlClient.SqlParameter Time = new System.Data.SqlClient.SqlParameter("@Time", mode.Time);

            if ((short)mode.typeState == 1)//добавление
            {
                this.Database.ExecuteSqlCommand("insert into mode   (name_dse,type_work,number_operation,version,number_trek,pornom,sodre,number_operationst_st,id_operation,P,t,pH,OPA_K,D,U,Time) values " +
                    "(@name_dse,@type_work,@number_operation,@version,@number_trek,@pornom,@sodre,0,@id_operation,@P,@t,@pH,@OPA_K,@D,@U,@Time)",
                    name_dse, type_work, number_operation, version, number_trek, pornom, sodre, id_operation, P, t, pH, OPA_K, D, U, Time);
            }
            if ((short)mode.typeState == 2)//изменение
            {
                this.Database.ExecuteSqlCommand("update mode  set version=@version,sodre=@sodre, P=@P, t=@t, pH=@pH, OPA_K=@OPA_K, D=@D, U=@U, Time=@Time, where id_operation=@id_operation AND number_trek=@number_trek AND pornom=@pornom "
                , version, sodre, P, t, pH, OPA_K, D, U, Time, id_operation, number_trek, pornom);
            }
            if ((short)mode.typeState == 3)//удаление
            {
                this.Database.ExecuteSqlCommand("DELETE from mode  where id_operation=@id_operation AND number_trek=@number_trek AND pornom=@pornom", id_operation, number_trek, pornom);
            }
        }
        public void StoreData(Rigging rigging, string newversion)
        {
            System.Data.SqlClient.SqlParameter id_operation = new System.Data.SqlClient.SqlParameter("@id_operation", rigging.id_operation);
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", rigging.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", rigging.type_work);
            System.Data.SqlClient.SqlParameter number_operation = new System.Data.SqlClient.SqlParameter("@number_operation", rigging.number_operation);
            System.Data.SqlClient.SqlParameter version = new System.Data.SqlClient.SqlParameter("@version", newversion);
            System.Data.SqlClient.SqlParameter number_trek = new System.Data.SqlClient.SqlParameter("@number_trek", rigging.number_trek);
            System.Data.SqlClient.SqlParameter pornom = new System.Data.SqlClient.SqlParameter("@pornom", rigging.pornom);
            System.Data.SqlClient.SqlParameter nn = new System.Data.SqlClient.SqlParameter("@nn", rigging.nn);
            System.Data.SqlClient.SqlParameter shc = new System.Data.SqlClient.SqlParameter("@shc", rigging.shc);
            System.Data.SqlClient.SqlParameter naimcto = new System.Data.SqlClient.SqlParameter("@naimcto", rigging.naimcto);
            System.Data.SqlClient.SqlParameter ipr = new System.Data.SqlClient.SqlParameter("@ipr", rigging.ipr);
            System.Data.SqlClient.SqlParameter number_operationst_st = new System.Data.SqlClient.SqlParameter("@number_operationst_st", rigging.number_operationst_st);

            if ((short)rigging.typeState == 1)//добавление
            {
                this.Database.ExecuteSqlCommand("insert into properti (name_dse,type_work,number_operation,version,number_trek,pornom,nn,shc,naimcto,ipr,number_operationst_st,id_operation)  values " +
                    "(@name_dse,@type_work,@number_operation,@version,@number_trek,@pornom,@nn,@shc,@naimcto,@ipr,0,@id_operation)",
                    name_dse, type_work, number_operation, version, number_trek, pornom, id_operation, nn, shc, naimcto, ipr);
            }
            if ((short)rigging.typeState == 2)//изменение
            {
                this.Database.ExecuteSqlCommand("update properti set version=@version,nn=@nn,shc=@shc,naimcto=@naimcto,ipr=@ipr where id_operation=@id_operation AND number_trek=@number_trek AND pornom=@pornom "
                , version, nn, shc, naimcto, ipr, id_operation, number_trek, pornom);
            }
            if ((short)rigging.typeState == 3)//удаление
            {
                this.Database.ExecuteSqlCommand("DELETE from properti where id_operation=@id_operation AND number_trek=@number_trek AND pornom=@pornom", id_operation, number_trek, pornom);
            }
        }
        public void StoreData(FreeText freeText, string newversion)
        {
            System.Data.SqlClient.SqlParameter id_operation = new System.Data.SqlClient.SqlParameter("@id_operation", freeText.id_operation);
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", freeText.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", freeText.type_work);
            System.Data.SqlClient.SqlParameter number_operation = new System.Data.SqlClient.SqlParameter("@number_operation", freeText.number_operation);
            System.Data.SqlClient.SqlParameter version = new System.Data.SqlClient.SqlParameter("@version", newversion);
            System.Data.SqlClient.SqlParameter pornom = new System.Data.SqlClient.SqlParameter("@pornom", freeText.pornom);
            System.Data.SqlClient.SqlParameter freetex = new System.Data.SqlClient.SqlParameter("@freetex", freeText.freetex);
            System.Data.SqlClient.SqlParameter st = new System.Data.SqlClient.SqlParameter("@st", freeText.st);

            if ((short)freeText.typeState == 1)//добавление
            {
                this.Database.ExecuteSqlCommand("insert into operationft (name_dse,type_work,number_operation,version,freetex,st,number_operationst_st,pornom,id_operation)  values " +
                    "(@name_dse,@type_work,@number_operation,@version,@freetex,@st,0,@pornom,@id_operation)",
                    name_dse, type_work, number_operation, version, freetex, st, pornom, id_operation);
            }
            if ((short)freeText.typeState == 2)//изменение
            {
                this.Database.ExecuteSqlCommand("update operationft set version=@version,freetex=@freetex,st=@st where id_operation=@id_operation AND pornom=@pornom "
                , version, freetex, st, id_operation, pornom);
            }
            if ((short)freeText.typeState == 3)//удаление
            {
                this.Database.ExecuteSqlCommand("DELETE from operationft where id_operation=@id_operation AND pornom=@pornom", id_operation, pornom);
            }
           
        }
        public void StoreData(Attention attention, string newversion)
        {
            System.Data.SqlClient.SqlParameter id_operation = new System.Data.SqlClient.SqlParameter("@id_operation", attention.id_operation);
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", attention.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", attention.type_work);
            System.Data.SqlClient.SqlParameter number_operation = new System.Data.SqlClient.SqlParameter("@number_operation", attention.number_operation);
            System.Data.SqlClient.SqlParameter version = new System.Data.SqlClient.SqlParameter("@version", newversion);
            System.Data.SqlClient.SqlParameter pornom = new System.Data.SqlClient.SqlParameter("@pornom", attention.pornom);
            System.Data.SqlClient.SqlParameter number_trek = new System.Data.SqlClient.SqlParameter("@number_trek", attention.number_trek);
            System.Data.SqlClient.SqlParameter freetex = new System.Data.SqlClient.SqlParameter("@freetex", attention.freetex);
            System.Data.SqlClient.SqlParameter st = new System.Data.SqlClient.SqlParameter("@st", attention.st);
            if ((short)attention.typeState == 1)//добавление
            {
                this.Database.ExecuteSqlCommand("insert into attention (name_dse,type_work,number_operation,version,number_trek,freetex,st,number_operationst_st,pornom,id_operation)  values " +
                    "(@name_dse,@type_work,@number_operation,@version,@number_trek,@freetex,@st,0,@pornom,@id_operation)",
                    name_dse, type_work, number_operation, version, number_trek, freetex, st, pornom, id_operation);
            }
            if ((short)attention.typeState == 2)//изменение
            {
                this.Database.ExecuteSqlCommand("update attention set version=@version,freetex=@freetex,st=@st where id_operation=@id_operation AND number_trek=@number_trek AND pornom=@pornom "
                , version, freetex, st, id_operation, number_trek, pornom);
            }
            if ((short)attention.typeState == 3)//удаление
            {
                this.Database.ExecuteSqlCommand("DELETE from attention where id_operation=@id_operation AND number_trek=@number_trek AND pornom=@pornom", id_operation, number_trek, pornom);
            }
        }

        public void StoreData(Operation operation,bool change)
        {
            System.Data.SqlClient.SqlParameter id_operation = new System.Data.SqlClient.SqlParameter("@id_operation", operation.id_operation);
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", operation.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", operation.type_work);
            System.Data.SqlClient.SqlParameter number_operation = new System.Data.SqlClient.SqlParameter("@number_operation", operation.number_operation);
            System.Data.SqlClient.SqlParameter idavtor = new System.Data.SqlClient.SqlParameter("@idavtor", operation.idavtor);
            System.Data.SqlClient.SqlParameter dpavtor = new System.Data.SqlClient.SqlParameter("@dpavtor", operation.dpavtor);
            System.Data.SqlClient.SqlParameter fioavtor = new System.Data.SqlClient.SqlParameter("@fioavtor", operation.fioavtor);
            System.Data.SqlClient.SqlParameter priz = new System.Data.SqlClient.SqlParameter("@priz", operation.priz);
            System.Data.SqlClient.SqlParameter punkt = new System.Data.SqlClient.SqlParameter("@punkt", operation.punkt);

            if (!change)//добавление
            {
                this.Database.ExecuteSqlCommand("insert into bdolog (name_dse,type_work,number_operation,idavtor,fioavtor,dpavtor,number_operationst_st,priz,punkt,id_operation)  values " +
                    "(@name_dse,@type_work,@number_operation,@idavtor,@fioavtor,@dpavtor,0,0,0,@id_operation)",
                    name_dse, type_work, number_operation, idavtor, fioavtor, dpavtor, id_operation);
            }
            if (change)//изменение
            {
                this.Database.ExecuteSqlCommand("update bdolog set idavtor=@idavtor,dpavtor=@dpavtor,fioavtor=@fioavtor " +
                    " WHERE name_dse=@name_dse  AND type_work=@type_work AND id_operation=@id_operation AND PRIZ=0"
                , name_dse, type_work, idavtor, fioavtor, dpavtor, id_operation);
            }
        }
        public void Updateversion(string version,Guid id_operation)
        {
            string[] tabels = { " operation ", " operationft ", " trek ", " attention ", " eqipment ", " details ", " material ", " mode ", " properti ", " image " };
            foreach(var tabell in tabels)
            {
                this.Database.ExecuteSqlCommand("update " + tabell + " set version=@version where id_operation=@id_operation", version, id_operation);
            }
        }
        public IEnumerable<Login> GetLogins(Operation operation)
        {
            System.Data.SqlClient.SqlParameter id_operation = new System.Data.SqlClient.SqlParameter("@id_operation", operation.id_operation);
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", operation.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", operation.type_work);

            return  this.Database.SqlQuery<Login>("select * from bdolog where name_dse=@name_dse and type_work=@type_work and id_operation=@id_operation",
                 name_dse, type_work, id_operation);
        }
        public IEnumerable<string> GetversionNow(string name_dse,string type_work,short number_operation)
        {
            System.Data.SqlClient.SqlParameter Name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", name_dse);
            System.Data.SqlClient.SqlParameter Type_work = new System.Data.SqlClient.SqlParameter("@type_work", type_work);
            System.Data.SqlClient.SqlParameter Number_operation = new System.Data.SqlClient.SqlParameter("@number_operation", number_operation);
            return this.Database.SqlQuery<string>("Select version = max(version) from  operation WHERE name_dse = @name_dse and type_work=@type_work and number_operation=@number_operation", Name_dse, Type_work, Number_operation);
        }
        public IEnumerable<string> GetversionNew(string name_dse, string type_work)
        {
            System.Data.SqlClient.SqlParameter Name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", name_dse);
            System.Data.SqlClient.SqlParameter Type_work = new System.Data.SqlClient.SqlParameter("@type_work", type_work);
            return this.Database.SqlQuery<string>("Select version = max(version) from  operation WHERE name_dse = @name_dse and type_work=@type_work", Name_dse, Type_work);
        }
        
        public IEnumerable<FreeText> SearchNullFreeText(FreeText freeText)
        {
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", freeText.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", freeText.type_work);
            if ((short)freeText.typeState == 1)
            {
                return this.Database.SqlQuery<FreeText>("select * from operationft where  name_dse = @name_dse and type_work=@type_work and number_operation=1000 and st='A' "
                 , name_dse, type_work);
            }
            if ((short)freeText.typeState == 3)
            {
                return this.Database.SqlQuery<FreeText>("select * from operationft where name_dse=@name_dse and type_work=@type_work and number_operation=1000 " +
                    "and version=(select max(version) from operationft where name_dse=@name_dse and type_work=@type_work and number_operation=1000)"
                 , name_dse, type_work);
            }
            else return null;
        }
        public void DeletNullFreeText(FreeText freeText)
        {
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", freeText.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", freeText.type_work);

            this.Database.ExecuteSqlCommand("DELETE * from operationft where name_dse = @name_dse and type_work=@type_work and number_operation=1000 and st='A'", name_dse, type_work);
        }
        public void InsertNullFreeText(FreeText freeText)
        {
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", freeText.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", freeText.type_work);
            System.Data.SqlClient.SqlParameter version = new System.Data.SqlClient.SqlParameter("@version", freeText.version);

            this.Database.ExecuteSqlCommand("insert into operationft (name_dse,type_work,number_operation,version,freetex,st,number_operationst_st,pornom,id_operation) VALUES" +
                "(@name_dse,@type_work,1000,@version,'','A',0,1,null)", name_dse, type_work, version);
        }
        public void StoreTotalInformation(TotalInformation information,string newversion)
        {
            System.Data.SqlClient.SqlParameter version = new System.Data.SqlClient.SqlParameter("@version", newversion);
            System.Data.SqlClient.SqlParameter name_dsed = new System.Data.SqlClient.SqlParameter("@name_dsed", information.name_dseD);
            System.Data.SqlClient.SqlParameter versiond = new System.Data.SqlClient.SqlParameter("@versiond", information.versiond);
            System.Data.SqlClient.SqlParameter dse_naim = new System.Data.SqlClient.SqlParameter("@dse_naim", information.DSE_NAIM);
            System.Data.SqlClient.SqlParameter izdel = new System.Data.SqlClient.SqlParameter("@izdel", information.izdel);
            System.Data.SqlClient.SqlParameter zakaz = new System.Data.SqlClient.SqlParameter("@zakaz", information.zakaz);
            System.Data.SqlClient.SqlParameter vid_pr = new System.Data.SqlClient.SqlParameter("@vid_pr", information.vid_pr);
            System.Data.SqlClient.SqlParameter nazvd = new System.Data.SqlClient.SqlParameter("@nazvd", information.nazvd);
            System.Data.SqlClient.SqlParameter osdokgtp = new System.Data.SqlClient.SqlParameter("@osdokgtp", information.osdokgtp);
            System.Data.SqlClient.SqlParameter nvun = new System.Data.SqlClient.SqlParameter("@nvun", information.nvun);
            System.Data.SqlClient.SqlParameter microfilm = new System.Data.SqlClient.SqlParameter("@microfilm", information.microfilm);
            System.Data.SqlClient.SqlParameter vd = new System.Data.SqlClient.SqlParameter("@vd", information.vd);
            System.Data.SqlClient.SqlParameter nd = new System.Data.SqlClient.SqlParameter("@nd", information.nd);
            System.Data.SqlClient.SqlParameter izv1 = new System.Data.SqlClient.SqlParameter("@izv1", information.izv1);
            System.Data.SqlClient.SqlParameter met_obrab = new System.Data.SqlClient.SqlParameter("@met_obrab", information.met_obrab);
            System.Data.SqlClient.SqlParameter osdok = new System.Data.SqlClient.SqlParameter("@osdok", information.osdok);
            System.Data.SqlClient.SqlParameter spread_nc = new System.Data.SqlClient.SqlParameter("@spread_nc", information.spread_nc);
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", information.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", information.type_work);


            if ((short)information.typeState == 2)
            {
                this.Database.ExecuteSqlCommand("update bod set version=@version,name_dsed=@name_dsed,versiond=@versiond" +
                    ",dse_naim=@dse_naim,izdel=@izdel,zakaz=@zakaz,vid_pr=@vid_pr,nazvd=@nazvd" +
                    ",osdokgtp=@osdokgtp,nvun=@nvun,microfilm=@microfilm,vd=@vd,nd=@nd,izv1=@izv1,met_obrab=@met_obrab,osdok=@osdok,spread_nc=@spread_nc" +
                    "WHERE name_dse=@name_dse AND type_work=@type_work"
                    , name_dse, type_work, version, name_dsed, dse_naim, versiond, izdel, zakaz, vid_pr, nazvd, osdokgtp
                    , nvun, microfilm, vd, nd, izv1, met_obrab, osdok, spread_nc);
            }
        }
        public void StoreTTTB(Tttb information, string newversion)
        {
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", information.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", information.type_work);
            System.Data.SqlClient.SqlParameter version = new System.Data.SqlClient.SqlParameter("@version", newversion);
            System.Data.SqlClient.SqlParameter punkt = new System.Data.SqlClient.SqlParameter("@punkt", information.punkt);
            System.Data.SqlClient.SqlParameter pornom = new System.Data.SqlClient.SqlParameter("@pornom", information.pornom);
            System.Data.SqlClient.SqlParameter tt = new System.Data.SqlClient.SqlParameter("@tt", information.tt);
            System.Data.SqlClient.SqlParameter priz = new System.Data.SqlClient.SqlParameter("@priz", information.priz);

            if ((short)information.typeState == 1)
            {
                this.Database.ExecuteSqlCommand("insert into permission (name_dse,type_work,version,punkt,pornom,tt,priz) values " +
                "(@name_dse,@type_work,@version,@version,@punkt,@pornom,@tt,@priz)", name_dse, type_work, version, punkt, pornom, tt, priz);
            }
            if ((short)information.typeState == 2)
            {
                this.Database.ExecuteSqlCommand("update permission set version=@version,punkt=@punkt,pornom=@pornom,tt=@tt,priz=@priz" +
                    "WHERE name_dse=@name_dse AND type_work=@type_work AND punkt=@punkt"
                    , name_dse, type_work, version, punkt, pornom, tt, priz);
            }
            if ((short)information.typeState == 3)
            {
                this.Database.ExecuteSqlCommand("DELETE from permission " +
                    "WHERE name_dse=@name_dse AND type_work=@type_work AND punkt=@punkt"
                    , name_dse, type_work, punkt, priz);
            }
        }
        public IEnumerable<Tttb> SearchNullTTTB(Tttb information)
        {
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", information.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", information.type_work);

            return this.Database.SqlQuery<Tttb>("select * from permission where  name_dse = @name_dse and type_work=@type_work and (priz=1 or priz=2 or priz=3) and punkt='A' "
                 , name_dse, type_work);
        }
        public  void DeleteNullTTTB(Tttb information)
        {
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", information.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", information.type_work);

            this.Database.ExecuteSqlCommand("Delete from permission where  name_dse = @name_dse and type_work=@type_work and (priz=1 or priz=2 or priz=3) and punkt='A' "
                 , name_dse, type_work);
        }
        public IEnumerable<Login> SearchAvtor(Tttb information)//?????
        {
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", information.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", information.type_work);
            System.Data.SqlClient.SqlParameter punkt = new System.Data.SqlClient.SqlParameter("@punkt", information.punkt);

            return this.Database.SqlQuery<Login>("select * from bdolog where  name_dse = @name_dse and type_work=@type_work and (priz=1 or priz=2 or priz=3) and punkt=@punkt "
                 , name_dse, type_work, punkt);
        }
        public void UpdateTTTB(Tttb information,string version)
        {
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", information.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", information.type_work);
            System.Data.SqlClient.SqlParameter idavtor = new System.Data.SqlClient.SqlParameter("@idavtor", information.idavtor);
            System.Data.SqlClient.SqlParameter dpavtor = new System.Data.SqlClient.SqlParameter("@dpavtor", information.dpavtor);
            System.Data.SqlClient.SqlParameter fioavtor = new System.Data.SqlClient.SqlParameter("@fioavtor", information.fioavtor);
            System.Data.SqlClient.SqlParameter punkt = new System.Data.SqlClient.SqlParameter("@punkt", information.punkt);
            System.Data.SqlClient.SqlParameter priz = new System.Data.SqlClient.SqlParameter("@priz", information.priz);

            this.Database.ExecuteSqlCommand("'update bdolog  set idavtor=@idavtor,dpavtor=@dpavtor,fioavtor=@fioavtor " +
                "where  name_dse = @name_dse and type_work=@type_work and (priz=1 or priz=2 or priz=3) and punkt=@punkt "
                 , name_dse, type_work, idavtor, dpavtor, fioavtor, punkt);
        }
        public void AddTTTB(Tttb information, string version)
        {
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", information.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", information.type_work);
            System.Data.SqlClient.SqlParameter idavtor = new System.Data.SqlClient.SqlParameter("@idavtor", information.idavtor);
            System.Data.SqlClient.SqlParameter dpavtor = new System.Data.SqlClient.SqlParameter("@dpavtor", information.dpavtor);
            System.Data.SqlClient.SqlParameter fioavtor = new System.Data.SqlClient.SqlParameter("@fioavtor", information.fioavtor);
            System.Data.SqlClient.SqlParameter punkt = new System.Data.SqlClient.SqlParameter("@punkt", information.punkt);
            System.Data.SqlClient.SqlParameter priz = new System.Data.SqlClient.SqlParameter("@priz", information.priz);

            this.Database.ExecuteSqlCommand("insert into bdolog " +
                "(name_dse, type_work, idavtor, dpavtor, fioavtor, punkt, priz) VALUES " +
                "(@name_dse, @type_work, @idavtor, @dpavtor, @fioavtor, @punkt, @priz)"
                 , name_dse, type_work, idavtor, dpavtor, fioavtor, punkt, priz);
        }
        public void Updateversion(Tttb tttb, string newversion)
        {
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", tttb.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", tttb.type_work);
            System.Data.SqlClient.SqlParameter version = new System.Data.SqlClient.SqlParameter("@version", newversion);
            System.Data.SqlClient.SqlParameter punkt = new System.Data.SqlClient.SqlParameter("@punkt", tttb.punkt);
            System.Data.SqlClient.SqlParameter priz = new System.Data.SqlClient.SqlParameter("@priz", tttb.priz);

            this.Database.ExecuteSqlCommand("update bdttp set version=@version" +
                "where name_dse = @name_dse and type_work=@type_work and priz=@priz and punkt=@punkt "
                , name_dse, type_work, version, punkt, priz);
        }
        public void AddNullTttb(Tttb tttb)
        {
            System.Data.SqlClient.SqlParameter name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", tttb.name_dse);
            System.Data.SqlClient.SqlParameter type_work = new System.Data.SqlClient.SqlParameter("@type_work", tttb.type_work);
            System.Data.SqlClient.SqlParameter version = new System.Data.SqlClient.SqlParameter("@version", tttb.version);
            System.Data.SqlClient.SqlParameter punkt = new System.Data.SqlClient.SqlParameter("@punkt", tttb.punkt);
            System.Data.SqlClient.SqlParameter priz = new System.Data.SqlClient.SqlParameter("@priz", tttb.priz);

            this.Database.ExecuteSqlCommand("insert into bdttp "+
                "(name_dse,type_work,version,punkt,pornom,tt,priz) VALUES " +
                "(@name_dse,@type_work,@version,'A',1,'',priz)"
                , name_dse, type_work, version, punkt, priz);
        }
        public void AddMethodCreatTtpOrNotice(string name_dse, string type_work,int regim,string fio,string login,string no,string po,int prgrtp)
        {
            System.Data.SqlClient.SqlParameter Name_dse = new System.Data.SqlClient.SqlParameter("@name_dse", name_dse);
            System.Data.SqlClient.SqlParameter Type_work = new System.Data.SqlClient.SqlParameter("@type_work", type_work);
            System.Data.SqlClient.SqlParameter Regim = new System.Data.SqlClient.SqlParameter("@Regim", regim);
            System.Data.SqlClient.SqlParameter Fio = new System.Data.SqlClient.SqlParameter("@fio", fio);
            System.Data.SqlClient.SqlParameter Login = new System.Data.SqlClient.SqlParameter("@login", login);
            System.Data.SqlClient.SqlParameter NO = new System.Data.SqlClient.SqlParameter("@NO", no);
            System.Data.SqlClient.SqlParameter PO = new System.Data.SqlClient.SqlParameter("@PO", po);
            System.Data.SqlClient.SqlParameter Prgrtp = new System.Data.SqlClient.SqlParameter("@prgrtp", prgrtp);

            this.Database.ExecuteSqlCommand(
               " if not exists(select no FROM  TLP  WHERE  name_dse = @name_dse AND type_work = @type_work)"+
             "begin " +
              "if @regim = 1" +
               "begin " +

            "    insert into  TLP" +
            "(name_dse, type_work, AVTOR, NO, PO, PGOTTP, SIGNAVTOR, NAKT, cex, IDAVTOR, prgrtp," +
            "cexakti, creationdata, stzak, guid) " +
            "values(@name_dse, @type_work, @FIO, @no, @po, 1, '', '', '', @LOGin, @prgrtp, '', getdate(), 0, newid()) " +

            "    insert into  bod" +
            "(name_dse, type_work, version, name_dsed, versiond, dse_naim, izdel, zakaz, vid_pr, naimc, nn, bvz," +
            "prr, v1d, nazvd, osdokgtp, nvun, ki, en, Microfilm, vd, nd, izv1, nr, massa," +
            "ei, met_obrab, osdok, spread_nc) " +
             "   values(@name_dse, @type_work, '00', @name_dse, '', '', '', ''," +
              "            '', '', '', '', '', 0, '', '', '', '00000', 0, ' ', 0, '', '', 0, 0, 0, 0, '', '123') " +

              "  insert into  operationP values(@name_dse, @type_work,'00',0,null,' '," +
            "@fio,@login,null,'',@no,@po,getdate(),0, newid())  " +
            "   end" +
             " else" +
               " Exec  ZAPIZARS @name_dse, @type_work, @fio,1,@login,@NO,@PO " +
             "END"
                 , Name_dse, Type_work, Regim, Fio, Login, NO, PO, Prgrtp);
        }
    }
}
