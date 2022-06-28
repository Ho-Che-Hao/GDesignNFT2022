using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using GDesign2022NFT.Controllers;
using GDesign2022NFT.ViewModel.UserVMs;
using GDesign2022NFT.Model;
using GDesign2022NFT.DataAccess;


namespace GDesign2022NFT.Test
{
    [TestClass]
    public class UserControllerTest
    {
        private UserController _controller;
        private string _seed;

        public UserControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<UserController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as UserListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(UserVM));

            UserVM vm = rv.Model as UserVM;
            User v = new User();
			
            v.ID = 46;
            v.Name = "QZh";
            v.Email = "iGsz1n6QXSlFTPb";
            v.Phone = "cvm3CWBXPNUicUjnWC";
            v.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Native;
            v.SchoolName = "dmpQrClRAeZMRKy8CNVjaRlxFn4dCR7i8q2T6";
            v.SchoolDepartment = "2GPJvWq";
            v.SchoolGrade = "pSFnvCZKR";
            v.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.Avtivity;
            v.Md5Code = "4th4X3W1VoaJ";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<User>().Find(v.ID);
				
                Assert.AreEqual(data.ID, 46);
                Assert.AreEqual(data.Name, "QZh");
                Assert.AreEqual(data.Email, "iGsz1n6QXSlFTPb");
                Assert.AreEqual(data.Phone, "cvm3CWBXPNUicUjnWC");
                Assert.AreEqual(data.IsForeigner, GDesign2022NFT.Model.ForeignerTypeEnum.Native);
                Assert.AreEqual(data.SchoolName, "dmpQrClRAeZMRKy8CNVjaRlxFn4dCR7i8q2T6");
                Assert.AreEqual(data.SchoolDepartment, "2GPJvWq");
                Assert.AreEqual(data.SchoolGrade, "pSFnvCZKR");
                Assert.AreEqual(data.AvtivityStatus, GDesign2022NFT.Model.AvtivityStatus.Avtivity);
                Assert.AreEqual(data.Md5Code, "4th4X3W1VoaJ");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            User v = new User();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ID = 46;
                v.Name = "QZh";
                v.Email = "iGsz1n6QXSlFTPb";
                v.Phone = "cvm3CWBXPNUicUjnWC";
                v.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Native;
                v.SchoolName = "dmpQrClRAeZMRKy8CNVjaRlxFn4dCR7i8q2T6";
                v.SchoolDepartment = "2GPJvWq";
                v.SchoolGrade = "pSFnvCZKR";
                v.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.Avtivity;
                v.Md5Code = "4th4X3W1VoaJ";
                context.Set<User>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(UserVM));

            UserVM vm = rv.Model as UserVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new User();
            v.ID = vm.Entity.ID;
       		
            v.Name = "qXBDILJhsxk0";
            v.Email = "ngkK";
            v.Phone = "iFrgo2Jprdrz8SSX5M";
            v.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Foreigner;
            v.SchoolName = "5E4szETbgv0CjA4";
            v.SchoolDepartment = "X53";
            v.SchoolGrade = "m0ehx";
            v.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.NotAvtivity;
            v.Md5Code = "vvgIgji19wQ";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ID", "");
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.Email", "");
            vm.FC.Add("Entity.Phone", "");
            vm.FC.Add("Entity.IsForeigner", "");
            vm.FC.Add("Entity.SchoolName", "");
            vm.FC.Add("Entity.SchoolDepartment", "");
            vm.FC.Add("Entity.SchoolGrade", "");
            vm.FC.Add("Entity.AvtivityStatus", "");
            vm.FC.Add("Entity.Md5Code", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<User>().Find(v.ID);
 				
                Assert.AreEqual(data.Name, "qXBDILJhsxk0");
                Assert.AreEqual(data.Email, "ngkK");
                Assert.AreEqual(data.Phone, "iFrgo2Jprdrz8SSX5M");
                Assert.AreEqual(data.IsForeigner, GDesign2022NFT.Model.ForeignerTypeEnum.Foreigner);
                Assert.AreEqual(data.SchoolName, "5E4szETbgv0CjA4");
                Assert.AreEqual(data.SchoolDepartment, "X53");
                Assert.AreEqual(data.SchoolGrade, "m0ehx");
                Assert.AreEqual(data.AvtivityStatus, GDesign2022NFT.Model.AvtivityStatus.NotAvtivity);
                Assert.AreEqual(data.Md5Code, "vvgIgji19wQ");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            User v = new User();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ID = 46;
                v.Name = "QZh";
                v.Email = "iGsz1n6QXSlFTPb";
                v.Phone = "cvm3CWBXPNUicUjnWC";
                v.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Native;
                v.SchoolName = "dmpQrClRAeZMRKy8CNVjaRlxFn4dCR7i8q2T6";
                v.SchoolDepartment = "2GPJvWq";
                v.SchoolGrade = "pSFnvCZKR";
                v.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.Avtivity;
                v.Md5Code = "4th4X3W1VoaJ";
                context.Set<User>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(UserVM));

            UserVM vm = rv.Model as UserVM;
            v = new User();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<User>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            User v = new User();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ID = 46;
                v.Name = "QZh";
                v.Email = "iGsz1n6QXSlFTPb";
                v.Phone = "cvm3CWBXPNUicUjnWC";
                v.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Native;
                v.SchoolName = "dmpQrClRAeZMRKy8CNVjaRlxFn4dCR7i8q2T6";
                v.SchoolDepartment = "2GPJvWq";
                v.SchoolGrade = "pSFnvCZKR";
                v.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.Avtivity;
                v.Md5Code = "4th4X3W1VoaJ";
                context.Set<User>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            User v1 = new User();
            User v2 = new User();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 46;
                v1.Name = "QZh";
                v1.Email = "iGsz1n6QXSlFTPb";
                v1.Phone = "cvm3CWBXPNUicUjnWC";
                v1.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Native;
                v1.SchoolName = "dmpQrClRAeZMRKy8CNVjaRlxFn4dCR7i8q2T6";
                v1.SchoolDepartment = "2GPJvWq";
                v1.SchoolGrade = "pSFnvCZKR";
                v1.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.Avtivity;
                v1.Md5Code = "4th4X3W1VoaJ";
                v2.ID = 61;
                v2.Name = "qXBDILJhsxk0";
                v2.Email = "ngkK";
                v2.Phone = "iFrgo2Jprdrz8SSX5M";
                v2.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Foreigner;
                v2.SchoolName = "5E4szETbgv0CjA4";
                v2.SchoolDepartment = "X53";
                v2.SchoolGrade = "m0ehx";
                v2.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.NotAvtivity;
                v2.Md5Code = "vvgIgji19wQ";
                context.Set<User>().Add(v1);
                context.Set<User>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(UserBatchVM));

            UserBatchVM vm = rv.Model as UserBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<User>().Find(v1.ID);
                var data2 = context.Set<User>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            User v1 = new User();
            User v2 = new User();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 46;
                v1.Name = "QZh";
                v1.Email = "iGsz1n6QXSlFTPb";
                v1.Phone = "cvm3CWBXPNUicUjnWC";
                v1.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Native;
                v1.SchoolName = "dmpQrClRAeZMRKy8CNVjaRlxFn4dCR7i8q2T6";
                v1.SchoolDepartment = "2GPJvWq";
                v1.SchoolGrade = "pSFnvCZKR";
                v1.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.Avtivity;
                v1.Md5Code = "4th4X3W1VoaJ";
                v2.ID = 61;
                v2.Name = "qXBDILJhsxk0";
                v2.Email = "ngkK";
                v2.Phone = "iFrgo2Jprdrz8SSX5M";
                v2.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Foreigner;
                v2.SchoolName = "5E4szETbgv0CjA4";
                v2.SchoolDepartment = "X53";
                v2.SchoolGrade = "m0ehx";
                v2.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.NotAvtivity;
                v2.Md5Code = "vvgIgji19wQ";
                context.Set<User>().Add(v1);
                context.Set<User>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(UserBatchVM));

            UserBatchVM vm = rv.Model as UserBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<User>().Find(v1.ID);
                var data2 = context.Set<User>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as UserListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }


    }
}
