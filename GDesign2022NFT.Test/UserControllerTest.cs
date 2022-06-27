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
			
            v.ID = 58;
            v.Name = "SoAQG0PFauC9";
            v.Email = "Ar8oqzbit";
            v.Phone = "6cyM7";
            v.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Native;
            v.SchoolName = "JKZTY";
            v.SchoolDepartment = "FDJBNjcgGOCXehYM5";
            v.SchoolGrade = "WFzI4A";
            v.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.NotAvtivity;
            v.Md5Code = "e60xrGVPfoZ5Ndpm16n";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<User>().Find(v.ID);
				
                Assert.AreEqual(data.ID, 58);
                Assert.AreEqual(data.Name, "SoAQG0PFauC9");
                Assert.AreEqual(data.Email, "Ar8oqzbit");
                Assert.AreEqual(data.Phone, "6cyM7");
                Assert.AreEqual(data.IsForeigner, GDesign2022NFT.Model.ForeignerTypeEnum.Native);
                Assert.AreEqual(data.SchoolName, "JKZTY");
                Assert.AreEqual(data.SchoolDepartment, "FDJBNjcgGOCXehYM5");
                Assert.AreEqual(data.SchoolGrade, "WFzI4A");
                Assert.AreEqual(data.AvtivityStatus, GDesign2022NFT.Model.AvtivityStatus.NotAvtivity);
                Assert.AreEqual(data.Md5Code, "e60xrGVPfoZ5Ndpm16n");
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
       			
                v.ID = 58;
                v.Name = "SoAQG0PFauC9";
                v.Email = "Ar8oqzbit";
                v.Phone = "6cyM7";
                v.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Native;
                v.SchoolName = "JKZTY";
                v.SchoolDepartment = "FDJBNjcgGOCXehYM5";
                v.SchoolGrade = "WFzI4A";
                v.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.NotAvtivity;
                v.Md5Code = "e60xrGVPfoZ5Ndpm16n";
                context.Set<User>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(UserVM));

            UserVM vm = rv.Model as UserVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new User();
            v.ID = vm.Entity.ID;
       		
            v.Name = "vj5B3XFr7ls6LIJ";
            v.Email = "wUTDt6XHONrZxfm";
            v.Phone = "TDFvB8pA95VJM1pdeSv";
            v.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Native;
            v.SchoolName = "B4QQIpDr";
            v.SchoolDepartment = "9wP1igNxouauo2b";
            v.SchoolGrade = "dLeI0nspTpAKsR8kV4";
            v.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.Avtivity;
            v.Md5Code = "Z6BB2j";
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
 				
                Assert.AreEqual(data.Name, "vj5B3XFr7ls6LIJ");
                Assert.AreEqual(data.Email, "wUTDt6XHONrZxfm");
                Assert.AreEqual(data.Phone, "TDFvB8pA95VJM1pdeSv");
                Assert.AreEqual(data.IsForeigner, GDesign2022NFT.Model.ForeignerTypeEnum.Native);
                Assert.AreEqual(data.SchoolName, "B4QQIpDr");
                Assert.AreEqual(data.SchoolDepartment, "9wP1igNxouauo2b");
                Assert.AreEqual(data.SchoolGrade, "dLeI0nspTpAKsR8kV4");
                Assert.AreEqual(data.AvtivityStatus, GDesign2022NFT.Model.AvtivityStatus.Avtivity);
                Assert.AreEqual(data.Md5Code, "Z6BB2j");
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
        		
                v.ID = 58;
                v.Name = "SoAQG0PFauC9";
                v.Email = "Ar8oqzbit";
                v.Phone = "6cyM7";
                v.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Native;
                v.SchoolName = "JKZTY";
                v.SchoolDepartment = "FDJBNjcgGOCXehYM5";
                v.SchoolGrade = "WFzI4A";
                v.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.NotAvtivity;
                v.Md5Code = "e60xrGVPfoZ5Ndpm16n";
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
				
                v.ID = 58;
                v.Name = "SoAQG0PFauC9";
                v.Email = "Ar8oqzbit";
                v.Phone = "6cyM7";
                v.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Native;
                v.SchoolName = "JKZTY";
                v.SchoolDepartment = "FDJBNjcgGOCXehYM5";
                v.SchoolGrade = "WFzI4A";
                v.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.NotAvtivity;
                v.Md5Code = "e60xrGVPfoZ5Ndpm16n";
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
				
                v1.ID = 58;
                v1.Name = "SoAQG0PFauC9";
                v1.Email = "Ar8oqzbit";
                v1.Phone = "6cyM7";
                v1.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Native;
                v1.SchoolName = "JKZTY";
                v1.SchoolDepartment = "FDJBNjcgGOCXehYM5";
                v1.SchoolGrade = "WFzI4A";
                v1.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.NotAvtivity;
                v1.Md5Code = "e60xrGVPfoZ5Ndpm16n";
                v2.ID = 41;
                v2.Name = "vj5B3XFr7ls6LIJ";
                v2.Email = "wUTDt6XHONrZxfm";
                v2.Phone = "TDFvB8pA95VJM1pdeSv";
                v2.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Native;
                v2.SchoolName = "B4QQIpDr";
                v2.SchoolDepartment = "9wP1igNxouauo2b";
                v2.SchoolGrade = "dLeI0nspTpAKsR8kV4";
                v2.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.Avtivity;
                v2.Md5Code = "Z6BB2j";
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
				
                v1.ID = 58;
                v1.Name = "SoAQG0PFauC9";
                v1.Email = "Ar8oqzbit";
                v1.Phone = "6cyM7";
                v1.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Native;
                v1.SchoolName = "JKZTY";
                v1.SchoolDepartment = "FDJBNjcgGOCXehYM5";
                v1.SchoolGrade = "WFzI4A";
                v1.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.NotAvtivity;
                v1.Md5Code = "e60xrGVPfoZ5Ndpm16n";
                v2.ID = 41;
                v2.Name = "vj5B3XFr7ls6LIJ";
                v2.Email = "wUTDt6XHONrZxfm";
                v2.Phone = "TDFvB8pA95VJM1pdeSv";
                v2.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Native;
                v2.SchoolName = "B4QQIpDr";
                v2.SchoolDepartment = "9wP1igNxouauo2b";
                v2.SchoolGrade = "dLeI0nspTpAKsR8kV4";
                v2.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.Avtivity;
                v2.Md5Code = "Z6BB2j";
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
