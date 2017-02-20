using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model;
using Extensions;

namespace BLL
{
    public class OrderAcceptServiceImp : AbstractService,IOrderAcceptService
    {
        private IProduceGeneralPlanRepository ProduceGeneralPlanrepository;

        //构造
        public OrderAcceptServiceImp(IProduceGeneralPlanRepository ProduceGeneralPlanrepository)
        {
            this.ProduceGeneralPlanrepository = ProduceGeneralPlanrepository;
        }


        #region IOrderAcceptService 成员

        /// <summary>
        /// 未接收的指示
        /// </summary>
        /// <param name="searchConditon"></param>
        /// <param name="pagex"></param>
        /// <returns></returns>
        public IEnumerable<VM_OrderAcceptShow> GetPlanNotAccept(VM_OrderAcceptSearch searchConditon, Paging pagex)
        {
            return ProduceGeneralPlanrepository.GetPlanNotAccept(searchConditon,pagex);
        }

        /// <summary>
        /// 接收字符串形式的客户订单，接收
        /// </summary>
        /// <param name="clientOrderID"></param>
        /// <returns></returns>
        public bool AcceptPlan(List<string> clientOrderIDs)
        {
            bool isAllSuccess = true;
            try
            {
                foreach (string item in clientOrderIDs)
                {
                    IEnumerable<ProduceGeneralPlan> products = ProduceGeneralPlanrepository.GetPlanByClientOrderID(item);
                    foreach (ProduceGeneralPlan product in products)
                    {
                        product.Status = Constant.GeneralPlanState.ACCEPTED;
                        ProduceGeneralPlanrepository.Update(product);
                    }

                }
            }
            catch (Exception)
            {
                isAllSuccess = false;
            }
           

            return isAllSuccess;
        }
        #endregion

        #region IOrderAcceptService 成员

        /// <summary>
        /// 接受一个计划
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool PlanAccept(ProduceGeneralPlan target)
        {
            target.Status =Constant.GeneralPlanState.ACCEPTED;
            return ProduceGeneralPlanrepository.Update(target);
        }

        /// <summary>
        /// 接收多个计划
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        [TransactionAop]
        public bool PlanAccept(List<ProduceGeneralPlan> target)
        {
            bool isAllSuc = true;
            try
            {
                foreach (var item in target)
                {
                    item.Status =Constant.GeneralPlanState.ACCEPTED;
                    ProduceGeneralPlanrepository.Update(item);
                }
            }
            catch (Exception)
            {
                isAllSuc = false;
            }
            return isAllSuc;
        }

        #endregion


        /// <summary>
        /// 插入测试数据
        /// </summary>
        /// <returns></returns>
        [TransactionAop]
        public bool TempInitDate()
        {
            bool bl = true;
            try
            {

                //List<ProduceGeneralPlan> xx = ProduceGeneralPlan.TestDate().ToList();
                //foreach (var x in xx)
                //{
                //    ProduceGeneralPlanrepository.Add(x);
                //}

                //List<MarketOrder> test = MarketOrder.TestDate().ToList();
                //foreach (var a in test)
                //{
                //    MarketOrderRepository.Add(a);
                //}
            }
            catch (Exception)
            {

                bl = false;
            }
            return bl;
        }


        /// <summary>
        /// 根据客户订单号，删除表 PD_GENERAL_PLAN 相关记录
        /// </summary>
        /// <param name="paraClientOrderID">客户订单号</param>
        /// <returns></returns>
        /// 创建者：冯吟夷
        public bool DeleteProduceGeneralPlanListService(string paraClientOrderID)
        {
            return this.ProduceGeneralPlanrepository.DeleteProduceGeneralPlanListRepository(paraClientOrderID);
        }


    } //end OrderAcceptServiceImp
}
