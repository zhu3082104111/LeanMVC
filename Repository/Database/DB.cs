using Model;
using Repository.database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.database
{
    public class DB:DbContext,ISql,IDB
    {
        private bool useTransaction = false;

        public DB()
            : base("name=LDKModel")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region decimalconfig
            
            foreach (Type classType in from t in Assembly.GetAssembly(typeof(DecimalPrecisionAttribute)).GetTypes()
                                       where t.IsClass && t.Namespace == "Model"
                                       select t)
            {
                foreach (var propAttr in classType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.GetCustomAttribute<DecimalPrecisionAttribute>() != null).Select(
                            p => new { prop = p, attr = p.GetCustomAttribute<DecimalPrecisionAttribute>(true) }))

                {

                    var entityConfig = modelBuilder.GetType().GetMethod("Entity").MakeGenericMethod(classType).Invoke(modelBuilder, null);
                    ParameterExpression param = ParameterExpression.Parameter(classType, "c");
                    Expression property = Expression.Property(param, propAttr.prop.Name);
                    LambdaExpression lambdaExpression = Expression.Lambda(property, true,
                                                                             new ParameterExpression[] { param });
                    DecimalPropertyConfiguration decimalConfig;
                    if (propAttr.prop.PropertyType.IsGenericType && propAttr.prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        MethodInfo methodInfo = entityConfig.GetType().GetMethods().Where(p => p.Name == "Property").ToList()[7];
                        decimalConfig = methodInfo.Invoke(entityConfig, new[] { lambdaExpression }) as DecimalPropertyConfiguration;
                    }
                    else
                    {
                        MethodInfo methodInfo = entityConfig.GetType().GetMethods().Where(p => p.Name == "Property").ToList()[6];
                        decimalConfig = methodInfo.Invoke(entityConfig, new[] { lambdaExpression }) as DecimalPropertyConfiguration;
                    }
                 
                    decimalConfig.HasPrecision(propAttr.attr.Precision, propAttr.attr.Scale);
                 }
             }
           
            #endregion

            modelBuilder.Entity<UserInfo>().ToTable("BI_UserInfo");
            modelBuilder.Entity<URoleInfo>().ToTable("BI_URoleInfo");
            modelBuilder.Entity<RoleInfo>().ToTable("BI_RoleInfo");
            modelBuilder.Entity<NoticeType>().ToTable("BI_NoticeType");
            modelBuilder.Entity<NoticeInfo>().ToTable("BI_NoticeInfo");
            modelBuilder.Entity<Notice>().ToTable("BI_Notice");
            modelBuilder.Entity<MenuInfo>().ToTable("BI_MenuInfo ");
            modelBuilder.Entity<DepartType>().ToTable("BI_DepartType");
            modelBuilder.Entity<Department>().ToTable("BI_Department");
            modelBuilder.Entity<UDepartment>().ToTable("BI_UDepartment");
            modelBuilder.Entity<ChainInfo>().ToTable("BI_ChainInfo ");
            modelBuilder.Entity<EditInfo>().ToTable("BI_EditInfo");
            modelBuilder.Entity<RoleChainInfo>().ToTable("BI_RoleChainInfo");
            modelBuilder.Entity<UserInfoLog>().ToTable("BI_UserInfoLog");
            modelBuilder.Entity<Log>().ToTable("BI_Log ");

        }
        //===============================================
        #region DB-Table


        public DbSet<UserInfo> UserInfo { get; set; }

        public DbSet<URoleInfo> URoleInfo { get; set; }

        public DbSet<RoleInfo> RoleInfo { get; set; }

        public DbSet<NoticeType> NoticeType { get; set; }

        public DbSet<NoticeInfo> NoticeInfo { get; set; }

        public DbSet<Notice> Notice { get; set; }

        public DbSet<MenuInfo> MenuInfo { get; set; }

        public DbSet<DepartType> DepartType { get; set; }

        public DbSet<Department> Department { get; set; }

        public DbSet<UDepartment> UDepartment { get; set; }

        public DbSet<ChainInfo> ChainInfo { get; set; }

        public DbSet<EditInfo> EditInfo { get; set; }

        public DbSet<RoleChainInfo> RoleChainInfo { get; set; }

        public DbSet<UserInfoLog> UserInfoLog { get; set; }

        public DbSet<Log> Log { get; set; }
        

        public DbSet<MasterDefiInfo> MasterDefiInfo { get; set; }
        public DbSet<TeamInfo> TeamInfo { get; set; }
        public DbSet<TeamMemberInfo> TeamMemberInfo { get; set; }
        public DbSet<UnitInfo> UnitInfo { get; set; }

        public DbSet<CodeMake> CodeMake { get; set; }

        public DbSet<MarketOrder> MarketOrder { get; set; }

        public DbSet<MarketClientInformation> MarketClientInformation { get; set; }

        public DbSet<MarketOrderDetailPrint> MarketOrderDetailPrint { get; set; }


        public DbSet<MCDeliveryOrder> MCDeliveryOrder { get; set; }

        public DbSet<MCDeliveryOrderDetail> MCDeliveryOrderDetail { get; set; }

        public DbSet<MCOutSourceOrder> MCOutSourceOrder { get; set; }

        public DbSet<MCOutSourceOrderDetail> MCOutSourceOrderDetail { get; set; }

        public DbSet<MCSupplierCnsmInfo> MCSupplierCnsmInfo { get; set; }

        public DbSet<MCSupplierOrder> MCSupplierOrder { get; set; }

        public DbSet<MCSupplierOrderDetail> MCSupplierOrderDetail { get; set; }




        public DbSet<AssemBill> AssemBill { get; set; }

        public DbSet<AssemBillDetail> AssemBillDetail { get; set; }

        public DbSet<ProduceMaterDetail> ProduceMaterDetail { get; set; }

        public DbSet<ProduceMaterRequest> ProduceMaterRequest { get; set; }



        public DbSet<AssemblyDispatch> AssemblyDispatch { get; set; }

        public DbSet<AssemblyDispatchDetail> AssemblyDispatchDetail { get; set; }

        public DbSet<MaterialDecompose> MaterialDecompose { get; set; }

        public DbSet<ProduceGeneralPlan> ProduceGeneralPlan { get; set; }

        public DbSet<ProduceRealDetail> ProductScheduleRealDetail { get; set; }

        public DbSet<ProduceSchedu> ProduceSchedu { get; set; }

        public DbSet<ProduceScheduDetail> ProduceScheduDetail { get; set; }

        public DbSet<ProduceInstruction> ProduceInstruction { get; set; }

        public DbSet<ProductWarehouse> ProductWarehouse { get; set; }

        public DbSet<ProcessDelivBill> ProcessDelivBill { get; set; }

        public DbSet<ProcessDelivery> ProcessDelivery { get; set; }

        public DbSet<ProcessDeliveryDetail> ProcessDeliveryDetail { get; set; }

        public DbSet<ProductWarehouseDetail> ProducWarehouseDetail { get; set; }

        public DbSet<AssistInstruction> AssistInstruction { get; set; }

        public DbSet<PurchaseInstruction> PurchaseInstruction { get; set; }



        public DbSet<AccInDetailRecord> AccInDetailRecord { get; set; }
        public DbSet<AccInRecord> AccInRecord { get; set; }
        public DbSet<AccOutRecord> AccOutRecord { get; set; }
        public DbSet<AccOutDetailRecord> AccOutRecordDetail { get; set; }
        public DbSet<Discard> Discard { get; set; }
        public DbSet<FinInDetailRecord> FinInDetailRecord { get; set; }
        public DbSet<FinInRecord> FinInRecord { get; set; }
        public DbSet<FinOutRecord> FinOutRecord { get; set; }
        public DbSet<FinOutDetailRecord> FinOutDetailRecord { get; set; }
        public DbSet<GiMaterial> GiMaterial { get; set; }
        public DbSet<GiReserve> GiReserve { get; set; }
        public DbSet<Reserve> Reserve { get; set; }
        public DbSet<ReserveDetail> ReserveDetail { get; set; }
        public DbSet<SemOutDetailRecord> SemOutDetailRecord { get; set; }
        public DbSet<WipInDetailRecord> WipInDetailRecord { get; set; }
        public DbSet<WipInRecord> WipInRecord { get; set; }
        public DbSet<WipOutRecord> WipOutRecord { get; set; }
        public DbSet<WipOutDetailRecord> WipOutDetailRecord { get; set; }
        public DbSet<SemInDetailRecord> SemInDetailRecord { get; set; }
        public DbSet<SemInRecord> SemInRecord { get; set; }
        public DbSet<SemOutRecord> SemOutRecord { get; set; }

        public DbSet<CompInfo> CompInfo { get; set; }
        public DbSet<CompMaterialInfo> CompMaterialInfo { get; set; }
        public DbSet<MachineInfo> MachineInfo { get; set; }
        public DbSet<MachineSchedule> MachineSchedule { get; set; }
        public DbSet<MachineWorktime> MachineWorktime { get; set; }
        public DbSet<PartInfo> PartInfo { get; set; }
        public DbSet<Process> Process { get; set; }
        public DbSet<ProcessDetail> ProcessDetail { get; set; }
        public DbSet<ProcessMachine> ProcessMachine { get; set; }
        public DbSet<ProdComposition> ProdComposition { get; set; }
        public DbSet<ProdInfo> ProdInfo { get; set; }
 
       
        #endregion
        //===============================================

        #region  IDB


        public IQueryable<T> GetList<T>(System.Linq.Expressions.Expression<Func<T, bool>> condition) where T : class
        {
            try
            {
                return base.Set<T>().Where(condition);
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }

        public void SetModified<T>(T item) where T : class
        {
            //this operation also attach item in object state manager
            base.Entry<T>(item).State = System.Data.EntityState.Modified;
        }

        public void SetEntityState<T>(T item, EntityState state) where T : class
        {
            //this operation also attach item in object state manager
            base.Entry<T>(item).State = state;
        }



        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current)
           where TEntity : class
        {
            //if it is not attached, attach original and set current values
            base.Entry<TEntity>(original).CurrentValues.SetValues(current);
        }


        public bool Add<T>(T entity) where T:class
        {
            try
            {

                if (entity != null)
                {
                    base.Set<T>().Add(entity);

                    return SaveChanges() > 0;

                }
                return false;
            }
            catch (Exception exp)
            {
                return false;
            }
        }

        public bool Update<T>(T entity) where T : class
        {
            try
            {

                base.Set<T>().Attach(entity);

                if (entity != (T)null)
                {
                    SetModified<T>(entity);
                }

                return SaveChanges() > 0;
            }
            catch (Exception exp)
            {
                return false;
            }
        }

        public bool Update<T>(T entity, string[] columns) where T : class
        {
            try
            {
                if (entity != (T)null)
                {
                    base.Set<T>().Attach(entity);
                    var stateEntry = base.Entry<T>(entity);

                    if (columns != null)
                    {
                        foreach (string s in columns)
                        {
                            stateEntry.Property(s).IsModified = true;
                        }
                    }
                }
                return SaveChanges() > 0;
            }
            catch (Exception exp)
            {
                return false;
            }
        }

        public bool Delete<T>(T entity) where T : class
        {
            try
            {
                base.Set<T>().Attach(entity);
                SetEntityState<T>(entity, EntityState.Deleted);
                //db.Entry<T>(entity).State = EntityState.Deleted;
                return SaveChanges() > 0;
            }
            catch (Exception exp)
            {
                return false;
            }
        }

        public bool BatchDelete<T>(System.Linq.Expressions.Expression<Func<T, bool>> criteria) where T : class
        {
            IEnumerable<T> records = base.Set<T>().Where(criteria);

            try
            {
                foreach (T record in records)
                {
                    Delete(record);
                }
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }

       
       
        public void BeginTransaction()
        {
            this.useTransaction = true;
           
        }
        public void EndTransaction()
        {
            this.useTransaction = false;
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    base.SaveChanges();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry =>
                              {
                                  entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                              });

                }
            } while (saveFailed);

        }

        public void RollbackChanges()
        {
            // set all entities in change tracker 
            // as 'unchanged state'
            base.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = System.Data.EntityState.Unchanged);
        }


        #endregion

        //===============================================

        #region ISql

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return base.Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommand(sqlCommand, parameters);

        }
        #endregion

        //===============================================
      


        public override int SaveChanges()
        {
            if (!this.useTransaction)
            {
                return base.SaveChanges();
            }
            return 1;
        }



        public void Dispose()
        {
            //if (db != null)
            //db.Dispose();
        }




    }

}
