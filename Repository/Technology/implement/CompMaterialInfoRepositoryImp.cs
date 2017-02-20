using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository.database;
using System.Collections;

namespace Repository
{
    class CompMaterialInfoRepositoryImp : AbstractRepository<DB, CompMaterialInfo>, ICompMaterialInfoRepository
    {

        #region ICompMaterialInfoRepository 成员

        public CompMaterialInfo SelectCompMaterialInfoForPrice(VM_AccInLoginStoreForTableShow accInLoginStore)
        {
            return base.First(a =>a.DelFlag=="0" && a.EffeFlag=="0" && a.CompId == accInLoginStore.CompID && a.PdtId == accInLoginStore.PdtID && a.CompType == "1" && a.ActivStrDt <= accInLoginStore.InDate && a.ActivEndDt >= accInLoginStore.InDate);
        }

        public CompMaterialInfo SelectCompMaterialInfoForPrice(VM_AccInRecordStoreForTableShow accInRecordStore)
        {
            return base.First(a =>a.DelFlag=="0" && a.EffeFlag=="0" && a.CompId == accInRecordStore.CompID && a.PdtId == accInRecordStore.PdtID && a.CompType == "1" && a.ActivStrDt <= accInRecordStore.InDate && a.ActivEndDt >= accInRecordStore.InDate);
        }

        public CompMaterialInfo SelectCompMaterialInfoForPrice(VM_WipInLoginStoreForTableShow wipInLoginStore)
        {
            return base.First(a => a.DelFlag == "0" && a.EffeFlag == "0" && a.CompId == wipInLoginStore.CompID && a.PdtId == wipInLoginStore.PdtID && a.CompType == "1" && a.ActivStrDt <= wipInLoginStore.InDate && a.ActivEndDt >= wipInLoginStore.InDate);
        }

        public CompMaterialInfo SelectCompMaterialInfoForPrice(VM_WipInRecordStoreForTableShow wipInRecordStore)
        {
            return base.First(a => a.DelFlag == "0" && a.EffeFlag == "0" && a.CompId == wipInRecordStore.CompID && a.PdtId == wipInRecordStore.PdtID && a.CompType == "1" && a.ActivStrDt <= wipInRecordStore.InDate && a.ActivEndDt >= wipInRecordStore.InDate);
        }

        public CompMaterialInfo SelectCompMaterialInfoForPrice(VM_SemInLoginStoreForTableShow semInLoginStore)
        {
            return base.First(a => a.DelFlag == "0" && a.EffeFlag == "0" && a.CompId == semInLoginStore.CompID && a.PdtId == semInLoginStore.PdtID && a.CompType == "1" && a.ActivStrDt <= semInLoginStore.InDate && a.ActivEndDt >= semInLoginStore.InDate);
        }


        #endregion
    }
}
