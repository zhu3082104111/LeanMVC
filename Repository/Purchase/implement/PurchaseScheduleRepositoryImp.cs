/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：OutsourcingScheduleRepositoryImp.cs
// 文件功能描述：外购产品进度画面的Repository实现
//      
// 创建标识：2013/10/21 刘云 新建
// 修改表示：2013/11/6 刘云 修改
// 修改描述：增加注释、修改名字
/*****************************************************************************/
using Model;
using Repository.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity.Infrastructure;

namespace Repository
{
   /* public class PurchaseScheduleRepositoryImp : AbstractRepository<DB, WaiGoujd>, IPurchaseScheduleRepository
    {
        //添加外购产品进度数据
        public bool Add(WaiGoujd purchaseSchedule)
        {
            return base.Add(purchaseSchedule);
        }
        //更新外购产品进度数据
        public bool update(WaiGoujd purchaseSchedule)
        {
            return this.Update(purchaseSchedule);
        }
        //更新外购产品进度数据
        public override bool Update(WaiGoujd purchaseSchedule)
        {
            try
            {
                ObservableCollection<WaiGoujd> tt = GetSet().Local;
                IEnumerable<WaiGoujd> cc = tt.AsEnumerable<WaiGoujd>();

                for (int i = 0; i < cc.Count(); i++)
                {
                    Db.Entry<WaiGoujd>(cc.ElementAt(i)).State = EntityState.Detached;
                }

                GetSet().Attach(purchaseSchedule);

                if (purchaseSchedule != (WaiGoujd)null)
                {
                    Db.SetModified<WaiGoujd>(purchaseSchedule);
                }
                return Db.SaveChanges() > 0;
            }
            catch (Exception exp)
            {
                return false;
            }
        }
        //根据外购单号进行删除
        public bool Delete(string id)
        {
            return base.ExecuteStoreCommand("DELETE FROM BI_WaiGoujd  WHERE WGdh={0}", id);
        }
    }*/
}
