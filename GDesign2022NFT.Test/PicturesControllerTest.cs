using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using GDesign2022NFT.Controllers;
using GDesign2022NFT.ViewModel.PicturesVMs;
using GDesign2022NFT.Model;
using GDesign2022NFT.DataAccess;


namespace GDesign2022NFT.Test
{
    [TestClass]
    public class PicturesControllerTest
    {
        private PicturesController _controller;
        private string _seed;

        public PicturesControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<PicturesController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as PicturesListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(PicturesVM));

            PicturesVM vm = rv.Model as PicturesVM;
            Pictures v = new Pictures();
			
            v.ID = 45;
            v.Md5Code = "L";
            v.PhotoId = AddFileAttachment();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Pictures>().Find(v.ID);
				
                Assert.AreEqual(data.ID, 45);
                Assert.AreEqual(data.Md5Code, "L");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            Pictures v = new Pictures();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ID = 45;
                v.Md5Code = "L";
                v.PhotoId = AddFileAttachment();
                context.Set<Pictures>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(PicturesVM));

            PicturesVM vm = rv.Model as PicturesVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new Pictures();
            v.ID = vm.Entity.ID;
       		
            v.Md5Code = "N4";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ID", "");
            vm.FC.Add("Entity.Md5Code", "");
            vm.FC.Add("Entity.PhotoId", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Pictures>().Find(v.ID);
 				
                Assert.AreEqual(data.Md5Code, "N4");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            Pictures v = new Pictures();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ID = 45;
                v.Md5Code = "L";
                v.PhotoId = AddFileAttachment();
                context.Set<Pictures>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(PicturesVM));

            PicturesVM vm = rv.Model as PicturesVM;
            v = new Pictures();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Pictures>().Find(v.ID);
                Assert.AreEqual(data.IsValid, false);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            Pictures v = new Pictures();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ID = 45;
                v.Md5Code = "L";
                v.PhotoId = AddFileAttachment();
                context.Set<Pictures>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            Pictures v1 = new Pictures();
            Pictures v2 = new Pictures();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 45;
                v1.Md5Code = "L";
                v1.PhotoId = AddFileAttachment();
                v2.ID = 31;
                v2.Md5Code = "N4";
                v2.PhotoId = v1.PhotoId; 
                context.Set<Pictures>().Add(v1);
                context.Set<Pictures>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(PicturesBatchVM));

            PicturesBatchVM vm = rv.Model as PicturesBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Pictures>().Find(v1.ID);
                var data2 = context.Set<Pictures>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            Pictures v1 = new Pictures();
            Pictures v2 = new Pictures();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ID = 45;
                v1.Md5Code = "L";
                v1.PhotoId = AddFileAttachment();
                v2.ID = 31;
                v2.Md5Code = "N4";
                v2.PhotoId = v1.PhotoId; 
                context.Set<Pictures>().Add(v1);
                context.Set<Pictures>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(PicturesBatchVM));

            PicturesBatchVM vm = rv.Model as PicturesBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Pictures>().Find(v1.ID);
                var data2 = context.Set<Pictures>().Find(v2.ID);
                Assert.AreEqual(data1.IsValid, false);
            Assert.AreEqual(data2.IsValid, false);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as PicturesListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddFileAttachment()
        {
            FileAttachment v = new FileAttachment();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.FileName = "DE2kwuAjwa3nb";
                v.FileExt = "Va3YQV";
                v.Path = "w7CdUMnXb";
                v.Length = 41;
                v.UploadTime = DateTime.Parse("2022-08-12 18:23:23");
                v.SaveMode = "Bf6dvVBsSr58pC8";
                v.ExtraInfo = "Ij523A38BN9";
                v.HandlerInfo = "DaKCWQDkQKYPGQNQ7A";
                context.Set<FileAttachment>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
