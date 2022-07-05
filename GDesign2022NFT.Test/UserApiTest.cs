using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using GDesign2022NFT.Controllers;
using GDesign2022NFT.ViewModel.API.UserVMs;
using GDesign2022NFT.Model;
using GDesign2022NFT.DataAccess;


namespace GDesign2022NFT.Test
{
    /*[TestClass]
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
            
            v.ID = 55;
            v.Name = "O0xVv5FAyB8nN8z";
            v.IdentyCode = "jrYMSu9jM";
            v.Email = "bSYluqgR";
            v.Phone = "tTOCC5Sd43UsX";
            v.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Foreigner;
            v.SchoolName = "Epqx1Rj7knZpgug2pN21y3gxpbxGQ6oQxxZUS1Os4wTmm7";
            v.SchoolDepartment = "awvRe7qzmKRvyA";
            v.SchoolGrade = "8Lod";
            v.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.Avtivity;
            v.Md5Code = "oWT";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<User>().Find(v.ID);
                
                Assert.AreEqual(data.ID, 55);
                Assert.AreEqual(data.Name, "O0xVv5FAyB8nN8z");
                Assert.AreEqual(data.IdentyCode, "jrYMSu9jM");
                Assert.AreEqual(data.Email, "bSYluqgR");
                Assert.AreEqual(data.Phone, "tTOCC5Sd43UsX");
                Assert.AreEqual(data.IsForeigner, GDesign2022NFT.Model.ForeignerTypeEnum.Foreigner);
                Assert.AreEqual(data.SchoolName, "Epqx1Rj7knZpgug2pN21y3gxpbxGQ6oQxxZUS1Os4wTmm7");
                Assert.AreEqual(data.SchoolDepartment, "awvRe7qzmKRvyA");
                Assert.AreEqual(data.SchoolGrade, "8Lod");
                Assert.AreEqual(data.AvtivityStatus, GDesign2022NFT.Model.AvtivityStatus.Avtivity);
                Assert.AreEqual(data.Md5Code, "oWT");
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
       			
                v.ID = 55;
                v.Name = "O0xVv5FAyB8nN8z";
                v.IdentyCode = "jrYMSu9jM";
                v.Email = "bSYluqgR";
                v.Phone = "tTOCC5Sd43UsX";
                v.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Foreigner;
                v.SchoolName = "Epqx1Rj7knZpgug2pN21y3gxpbxGQ6oQxxZUS1Os4wTmm7";
                v.SchoolDepartment = "awvRe7qzmKRvyA";
                v.SchoolGrade = "8Lod";
                v.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.Avtivity;
                v.Md5Code = "oWT";
                context.Set<User>().Add(v);
                context.SaveChanges();
            }

            UserApiVM vm = _controller.Wtm.CreateVM<UserApiVM>();
            var oldID = v.ID;
            v = new User();
            v.ID = oldID;
       		
            v.Name = "5OeYAnKiTYfe4eND2kVAeYomaGYUT9mohU7ETRk83ypNuzhM";
            v.IdentyCode = "X";
            v.Email = "myQ";
            v.Phone = "KCZQN27X3F6n90J2F7";
            v.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Foreigner;
            v.SchoolName = "Ff7XnCnC4Z8Dn";
            v.SchoolDepartment = "EUB3czN02uoPKTImMM";
            v.SchoolGrade = "t7SgW";
            v.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.Avtivity;
            v.Md5Code = "5HY3DcKPenFq5V";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ID", "");
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.IdentyCode", "");
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
 				
                Assert.AreEqual(data.Name, "5OeYAnKiTYfe4eND2kVAeYomaGYUT9mohU7ETRk83ypNuzhM");
                Assert.AreEqual(data.IdentyCode, "X");
                Assert.AreEqual(data.Email, "myQ");
                Assert.AreEqual(data.Phone, "KCZQN27X3F6n90J2F7");
                Assert.AreEqual(data.IsForeigner, GDesign2022NFT.Model.ForeignerTypeEnum.Foreigner);
                Assert.AreEqual(data.SchoolName, "Ff7XnCnC4Z8Dn");
                Assert.AreEqual(data.SchoolDepartment, "EUB3czN02uoPKTImMM");
                Assert.AreEqual(data.SchoolGrade, "t7SgW");
                Assert.AreEqual(data.AvtivityStatus, GDesign2022NFT.Model.AvtivityStatus.Avtivity);
                Assert.AreEqual(data.Md5Code, "5HY3DcKPenFq5V");
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
        		
                v.ID = 55;
                v.Name = "O0xVv5FAyB8nN8z";
                v.IdentyCode = "jrYMSu9jM";
                v.Email = "bSYluqgR";
                v.Phone = "tTOCC5Sd43UsX";
                v.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Foreigner;
                v.SchoolName = "Epqx1Rj7knZpgug2pN21y3gxpbxGQ6oQxxZUS1Os4wTmm7";
                v.SchoolDepartment = "awvRe7qzmKRvyA";
                v.SchoolGrade = "8Lod";
                v.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.Avtivity;
                v.Md5Code = "oWT";
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
				
                v1.ID = 55;
                v1.Name = "O0xVv5FAyB8nN8z";
                v1.IdentyCode = "jrYMSu9jM";
                v1.Email = "bSYluqgR";
                v1.Phone = "tTOCC5Sd43UsX";
                v1.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Foreigner;
                v1.SchoolName = "Epqx1Rj7knZpgug2pN21y3gxpbxGQ6oQxxZUS1Os4wTmm7";
                v1.SchoolDepartment = "awvRe7qzmKRvyA";
                v1.SchoolGrade = "8Lod";
                v1.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.Avtivity;
                v1.Md5Code = "oWT";
                v2.ID = 63;
                v2.Name = "5OeYAnKiTYfe4eND2kVAeYomaGYUT9mohU7ETRk83ypNuzhM";
                v2.IdentyCode = "X";
                v2.Email = "myQ";
                v2.Phone = "KCZQN27X3F6n90J2F7";
                v2.IsForeigner = GDesign2022NFT.Model.ForeignerTypeEnum.Foreigner;
                v2.SchoolName = "Ff7XnCnC4Z8Dn";
                v2.SchoolDepartment = "EUB3czN02uoPKTImMM";
                v2.SchoolGrade = "t7SgW";
                v2.AvtivityStatus = GDesign2022NFT.Model.AvtivityStatus.Avtivity;
                v2.Md5Code = "5HY3DcKPenFq5V";
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


    }*/
}
