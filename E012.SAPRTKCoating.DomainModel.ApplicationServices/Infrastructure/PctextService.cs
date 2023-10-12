using E012.DomainModelServer.Model.Entities.PCTEXT;
using E012.DomainModelServer.Model.Entities.PCTEXT.Shablone;
using E012.DomainModelServer.Model.Entities.SKAT;
using E012.SAPRTKCoating.EFDataSource.DBContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.SAPRTKCoating.DomainModel.ApplicationServices.Infrastructure
{
    
    public class PctextService : IDisposable
    {
        private LocalContextPCTEXT _context = null;

        public LocalContextPCTEXT LocalContext
        {
            get
            {
                if (_context == null)
                    _context = new LocalContextPCTEXT();
                return _context;
            }
        }
        /// <summary>
        /// Процедура получения дерева типовых операций по номеру массива
        /// </summary>
        /// <param name="nom_mas">Номер массива</param>
        /// <param name="operation">операция или переход</param>
        /// <returns></returns> 
        public List<TreeShablonOperationOrTransit> GetTreeTP(int nom_mas, bool operation)
        {
            if (operation)
                return LocalContext.GetTreeOperation(nom_mas).ToList();
            else
                return LocalContext.GetTreeTransit(nom_mas).ToList();
        }
        public List<T> GetTipTechnicalArrays<T>(int nom_mas, int tip,string cex)
        {
            return LocalContext.GetTipTechnicalArrays<T>(nom_mas, tip,cex).ToList();
        }
        /// <summary>
        /// Процедура получения списка технических массивов
        /// </summary>
        /// <returns></returns>       
        public List<TechnicalArrays> GetTechnicalArrays()
        {
            return LocalContext.GetTechnicalArrays().ToList();
        }
        public List<TreeShablone> GetTree(int nom_mas)
        {

            List<OperationShablone> operations = LocalContext.GetAllOperation(nom_mas).ToList();

            return operations.Select(x=>new TreeShablone(x,
                LocalContext.GetTipShabloneOperation<TrekShablone>(nom_mas, 3, (short)TypeChapter.TREK, x.id_operation, "0", 0).ToList()
                )).ToList();
        }
        public AllOperationInformation GetTipShabloneOperation(int nom_mas, Guid id_operationeration, int pornom)
        {
            AllOperationInformation result = new AllOperationInformation();
            result.operation = LocalContext.GetTipShabloneOperation<OperationShablone>(nom_mas, 1, (short)TypeChapter.OPERATION, id_operationeration, "0", pornom).FirstOrDefault();
            result.equipments = LocalContext.GetTipShabloneOperation<EquipmentShablone>(nom_mas, 3, (short)TypeChapter.EQUIPMENT, id_operationeration, "0", pornom).ToList();
            result.materials = LocalContext.GetTipShabloneOperation<MaterialShablone>(nom_mas, 3, (short)TypeChapter.MATERIAL, id_operationeration, "0", pornom).ToList();
            result.riggings = LocalContext.GetTipShabloneOperation<RiggingShablone>(nom_mas, 3, (short)TypeChapter.RIGGING, id_operationeration, "0", pornom).ToList();
            result.modes = LocalContext.GetTipShabloneOperation<ModeShablone>(nom_mas, 3, (short)TypeChapter.MODE, id_operationeration, "0", pornom).ToList();
            result.attention = LocalContext.GetTipShabloneOperation<FreeTextShablone>(nom_mas, 3, (short)TypeChapter.ATTENTION_RECORD, id_operationeration, "0", pornom).ToList();
            result.instruction = LocalContext.GetTipShabloneOperation<FreeTextShablone>(nom_mas, 3, (short)TypeChapter.DESIGNATION_RECORD, id_operationeration, "0", pornom).ToList();
            //IEnumerable<TrekShablone> treks = LocalContext.GetTipShabloneOperation<TrekShablone>(nom_mas, 3, (short)TypeChapter.TREK, id_operationeration, "0", pornom).ToList();
            //IEnumerable<ModeShablone> modestrek = LocalContext.GetTipShabloneOperation<ModeShablone>(nom_mas, 5, (short)TypeChapter.MODE, id_operationeration, number_trek, pornom).ToList();
            //IEnumerable<FreeTextTrekShablone> instructionsTrek = LocalContext.GetTipShabloneOperation<FreeTextTrekShablone>(nom_mas, 5, (short)TypeChapter.DESIGNATION_RECORD, id_operationeration, number_trek, pornom).ToList();
            //IEnumerable<RiggingShablone> riggingsTrek = LocalContext.GetTipShabloneOperation<RiggingShablone>(nom_mas, 5, (short)TypeChapter.RIGGING, id_operationeration, number_trek, pornom).ToList();
            //result.allTrekInformation = getAllTrekInformation(treks.Where(x=>x.number_trek.Trim() == number_trek.Trim()).ToList(), modestrek, instructionsTrek, riggingsTrek);

            return result;
        }
        public AllTrekInformation GetForTipShabloneTransition(int nom_mas, Guid id_operationeration, string number_trek, int pornom)
        {
            AllTrekInformation result = new AllTrekInformation();
            result.trek = LocalContext.GetTipShabloneOperation<TrekShablone>(nom_mas, 3, (short)TypeChapter.TREK, id_operationeration, number_trek, pornom).FirstOrDefault();
            result.modes = LocalContext.GetTipShabloneOperation<ModeShablone>(nom_mas, 5, (short)TypeChapter.MODE, id_operationeration, number_trek, pornom).ToList();
            result.freeTextTreks = LocalContext.GetTipShabloneOperation<FreeTextTrekShablone>(nom_mas, 5, (short)TypeChapter.DESIGNATION_RECORD, id_operationeration, number_trek, pornom).ToList();
            result.riggings = LocalContext.GetTipShabloneOperation<RiggingShablone>(nom_mas, 5, (short)TypeChapter.RIGGING, id_operationeration, number_trek, pornom).ToList();
            return result;
        }

        public AllTrekInformation GetAllShabloneTransition(int nom_mas,  Guid id_number_trek, int pornom)
        {
            AllTrekInformation result = new AllTrekInformation();
            result.trek = LocalContext.GetTipShabloneTransition<TrekShablone>(nom_mas, (short)TypeChapter.TREK, id_number_trek, pornom).FirstOrDefault();
            result.modes = LocalContext.GetTipShabloneTransition<ModeShablone>(nom_mas, (short)TypeChapter.MODE, id_number_trek, pornom).ToList();
            result.freeTextTreks = LocalContext.GetTipShabloneTransition<FreeTextTrekShablone>(nom_mas, (short)TypeChapter.DESIGNATION_RECORD, id_number_trek, pornom).ToList();
            result.riggings = LocalContext.GetTipShabloneTransition<RiggingShablone>(nom_mas, (short)TypeChapter.RIGGING, id_number_trek, pornom).ToList();
            return result;
        }
        //private List<AllTrekInformation> getAllTrekInformation(IEnumerable<TrekShablone> treks, IEnumerable<ModeShablone> modestrek, IEnumerable<FreeTextTrekShablone> instructionsTrek, IEnumerable<RiggingShablone> riggingsTrek)
        //{
        //    return treks.Select
        //            (x => new AllTrekInformation(
        //                x,
        //                modestrek.Where(m => m.number_trek.Trim() == x.number_trek.Trim()).ToList(),
        //                instructionsTrek.Where(m => m.number_trek.Trim() == x.number_trek.Trim()).ToList(),
        //                riggingsTrek.Where(m => m.number_trek.Trim() == x.number_trek.Trim()).ToList()
        //            )
        //            ).ToList();
        //}


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