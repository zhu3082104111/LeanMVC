using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository
{
    public interface ICompMaterialInfoRepository
     {
        //查询零件单价
        CompMaterialInfo SelectCompMaterialInfoForPrice(VM_AccInLoginStoreForTableShow accInLoginStore);

        //查询零件单价
        CompMaterialInfo SelectCompMaterialInfoForPrice(VM_WipInLoginStoreForTableShow wipInLoginStore);

        //查询零件单价
        CompMaterialInfo SelectCompMaterialInfoForPrice(VM_AccInRecordStoreForTableShow accInRecordStore);

        //查询零件单价
        CompMaterialInfo SelectCompMaterialInfoForPrice(VM_WipInRecordStoreForTableShow wipInRecordStore);

         //查询零件单价
        CompMaterialInfo SelectCompMaterialInfoForPrice(VM_SemInLoginStoreForTableShow semInLoginStore);
        


     }
}
