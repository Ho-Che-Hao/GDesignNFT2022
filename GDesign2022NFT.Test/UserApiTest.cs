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
    public class UserApiTest
    {
        private UserApiController _controller;
        private string _seed;

        public UserApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<UserApiController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new UserApiSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            UserApiVM vm = _controller.Wtm.CreateVM<UserApiVM>();
            User v = new User();
            
            v.ID = 21;
            v.Name = "Qk22";
            v.Email = "J1g60PAGU8V11";
            v.Phone = "oj30DSBjaYb";
            v.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Foreigner;
            v.SchoolName = "4CNHwzqh3";
            v.SchoolDepartment = "jJdDUJhMlXKBYyA";
            v.SchoolGrade = "amyIm821whV1R2";
            v.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.NotAvtivity;
            v.Md5Code = "WxzmoZ5RJUv81djWA7";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<User>().Find(v.ID);
                
                Assert.AreEqual(data.ID, 21);
                Assert.AreEqual(data.Name, "Qk22");
                Assert.AreEqual(data.Email, "J1g60PAGU8V11");
                Assert.AreEqual(data.Phone, "oj30DSBjaYb");
                Assert.AreEqual(data.IsForeigner, GDesign2022NFT.Model.ForeignerTypeEnum.Foreigner);
                Assert.AreEqual(data.SchoolName, "4CNHwzqh3");
                Assert.AreEqual(data.SchoolDepartment, "jJdDUJhMlXKBYyA");
                Assert.AreEqual(data.SchoolGrade, "amyIm821whV1R2");
                Assert.AreEqual(data.AvtivityStatus, GDesign2022NFT.Model.AvtivityStatus.NotAvtivity);
                Assert.AreEqual(data.Md5Code, "WxzmoZ5RJUv81djWA7");
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
       			
                v.ID = 21;
                v.Name = "Qk22";
                v.Email = "J1g60PAGU8V11";
                v.Phone = "oj30DSBjaYb";
                v.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Foreigner;
                v.SchoolName = "4CNHwzqh3";
                v.SchoolDepartment = "jJdDUJhMlXKBYyA";
                v.SchoolGrade = "amyIm821whV1R2";
                v.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.NotAvtivity;
                v.Md5Code = "WxzmoZ5RJUv81djWA7";
                context.Set<User>().Add(v);
                context.SaveChanges();
            }

            UserApiVM vm = _controller.Wtm.CreateVM<UserApiVM>();
            var oldID = v.ID;
            v = new User();
            v.ID = oldID;
       		
            v.Name = "iTxjgjq";
            v.Email = "K0kSsosIrmRGO";
            v.Phone = "q5TgE0ilQgxa6VFHIA1";
            v.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Native;
            v.SchoolName = "dvFyUguEzltwY";
            v.SchoolDepartment = "pE0fEF";
            v.SchoolGrade = "cbGdzXor7nP8gMMxIa";
            v.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.NotAvtivity;
            v.Md5Code = "syN9YW1SZGgF6Vftx";
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
            var rv = _controller.Edit(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<User>().Find(v.ID);
 				
                Assert.AreEqual(data.Name, "iTxjgjq");
                Assert.AreEqual(data.Email, "K0kSsosIrmRGO");
                Assert.AreEqual(data.Phone, "q5TgE0ilQgxa6VFHIA1");
                Assert.AreEqual(data.IsForeigner, GDesign2022NFT.Model.ForeignerTypeEnum.Native);
                Assert.AreEqual(data.SchoolName, "dvFyUguEzltwY");
                Assert.AreEqual(data.SchoolDepartment, "pE0fEF");
                Assert.AreEqual(data.SchoolGrade, "cbGdzXor7nP8gMMxIa");
                Assert.AreEqual(data.AvtivityStatus, GDesign2022NFT.Model.AvtivityStatus.NotAvtivity);
                Assert.AreEqual(data.Md5Code, "syN9YW1SZGgF6Vftx");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }

		[TestMethod]
        public void GetTest()
        {
            User v = new User();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ID = 21;
                v.Name = "Qk22";
                v.Email = "J1g60PAGU8V11";
                v.Phone = "oj30DSBjaYb";
                v.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Foreigner;
                v.SchoolName = "4CNHwzqh3";
                v.SchoolDepartment = "jJdDUJhMlXKBYyA";
                v.SchoolGrade = "amyIm821whV1R2";
                v.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.NotAvtivity;
                v.Md5Code = "WxzmoZ5RJUv81djWA7";
                context.Set<User>().Add(v);
                context.SaveChanges();
            }
            var rv = _controller.Get(v.ID.ToString());
            Assert.IsNotNull(rv);
        }

        [TestMethod]
        public void BatchDeleteTest()
        {
            User v1 = new User();
            User v2 = new User();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 21;
                v1.Name = "Qk22";
                v1.Email = "J1g60PAGU8V11";
                v1.Phone = "oj30DSBjaYb";
                v1.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Foreigner;
                v1.SchoolName = "4CNHwzqh3";
                v1.SchoolDepartment = "jJdDUJhMlXKBYyA";
                v1.SchoolGrade = "amyIm821whV1R2";
                v1.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.NotAvtivity;
                v1.Md5Code = "WxzmoZ5RJUv81djWA7";
                v2.ID = 58;
                v2.Name = "iTxjgjq";
                v2.Email = "K0kSsosIrmRGO";
                v2.Phone = "q5TgE0ilQgxa6VFHIA1";
                v2.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Native;
                v2.SchoolName = "dvFyUguEzltwY";
                v2.SchoolDepartment = "pE0fEF";
                v2.SchoolGrade = "cbGdzXor7nP8gMMxIa";
                v2.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.NotAvtivity;
                v2.Md5Code = "syN9YW1SZGgF6Vftx";
                context.Set<User>().Add(v1);
                context.Set<User>().Add(v2);
                context.SaveChanges();
            }

            var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<User>().Find(v1.ID);
                var data2 = context.Set<User>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }

            rv = _controller.BatchDelete(new string[] {});
            Assert.IsInstanceOfType(rv, typeof(OkResult));

        }


    }
}
