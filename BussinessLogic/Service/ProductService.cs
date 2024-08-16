using AutoMapper;
using BussinessLogic.DTO;
using BussinessLogic.IService;
using DataAccess.DAO;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Service
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly ProductDAO _productDAO;
        public ProductService(IMapper mapper, ProductDAO productDAO)
        {
            _mapper = mapper;
            _productDAO = productDAO;
        }

        public void DeleteProduct(int ProductID)
        {
            _productDAO.DeleteProduct(ProductID);
        }

        public ProductDTO GetProductById(int ProductID)
        {
            return _mapper.Map<ProductDTO>(_productDAO.GetProductById(ProductID));
        }

        public List<ProductDTO> GetProducts()
        {
            using (DataAccessContext context = new DataAccessContext())
            {
                var lstPr = _mapper.Map<List<ProductDTO>>(_productDAO.GetProducts());
                foreach (var pr in lstPr)
                {
                    try
                    {
                        pr.ProductPrice = context.ProductDetails.FirstOrDefault(x => x.ProductId == pr.ProductId).ProductDetailPrice;
                    }
                    catch (Exception)
                    {

                        pr.ProductPrice = 0;
                    }
                }

                return lstPr;
            }

        }
        public List<ProductDTO> GetProductsNotSale()
        {
            using (DataAccessContext context = new DataAccessContext())
            {
                var lstPr = _mapper.Map<List<ProductDTO>>(_productDAO.GetProductsNotSale());
                foreach (var pr in lstPr)
                {
                    try
                    {
                        pr.ProductPrice = context.ProductDetails.FirstOrDefault(x => x.ProductId == pr.ProductId).ProductDetailPrice;
                    }
                    catch (Exception)
                    {

                        pr.ProductPrice = 0;
                    }
                }

                return lstPr;
            }

        }
        public List<ProductDTO> GetProductsSale()
        {
            using (DataAccessContext context = new DataAccessContext())
            {
                var lstPr = _mapper.Map<List<ProductDTO>>(_productDAO.GetProductsSale());
                foreach (var pr in lstPr)
                {
                    try
                    {
                        var price = context.ProductDetails.FirstOrDefault(x => x.ProductId == pr.ProductId).ProductDetailPrice;
                        pr.ProductPrice = price;
                        pr.ProductSalePrice = price - (price / 100 * pr.SalePercent);
                    }
                    catch (Exception)
                    {
                        pr.ProductPrice = 0;
                    }
                }

                return lstPr;
            }
        }

        public List<ProductDTO> SearchProducts(string key)
        {
            using (DataAccessContext context = new DataAccessContext())
            {
                var lstPr = _mapper.Map<List<ProductDTO>>(_productDAO.SearchProduct(key));
                foreach (var pr in lstPr)
                {
                    try
                    {
                        var price = context.ProductDetails.FirstOrDefault(x => x.ProductId == pr.ProductId).ProductDetailPrice;
                        pr.ProductPrice = price;
                        pr.ProductSalePrice = price - (price / 100 * pr.SalePercent);
                    }
                    catch (Exception)
                    {
                        pr.ProductPrice = 0;
                    }
                }

                return lstPr;
            }
        }

        public void UpdateProduct(ProductDTO productDto)
        {
            _productDAO.UpdateProduct(_mapper.Map<Product>(productDto));
        }

        public int SaveProduct(ProductDTO productDto, List<ProductDetailDTO> productDetailDTOs)
        {
            return _productDAO.SaveProduct(_mapper.Map<Product>(productDto), _mapper.Map<List<ProductDetail>>(productDetailDTOs));
        }

        public ProductRequest GetProductByIdDetail(int ProductID)
        {
            using (DataAccessContext context = new DataAccessContext())
            {
                Product productExist = context.Products.FirstOrDefault(x => x.ProductId == ProductID);
                List<ProductDetail> productDetail = context.ProductDetails.Where(x => x.ProductId == ProductID).ToList();
                if (productExist != null && productDetail.Count() > 0)
                {
                    ProductRequest pr = new ProductRequest();
                    pr.Product = _mapper.Map<ProductDTO>(productExist);
                    pr.ProductDetails = _mapper.Map<List<ProductDetailDTO>>(productDetail);
                    return pr;
                }
                return null;
            }
        }

        public ProductDetailDTO GetProductDetailById(int productId)
        {
            using (DataAccessContext context = new DataAccessContext())
            {
                var detail = context.ProductDetails.FirstOrDefault(x => x.ProductDetailId == productId);
                return _mapper.Map<ProductDetailDTO>(detail);
            }
        }

        public void DeleteDetailProduct(int id)
        {
            _productDAO.DeleteDetailProduct(id);
        }

        public void EditProduct(ProductDTO product, List<ProductDetailDTO> productDetails)
        {
            _productDAO.SaveEditProduct(_mapper.Map<Product>(product), _mapper.Map<List<ProductDetail>>(productDetails));
        }

        public List<ProductDTO> GetProductsById(int id)
        {
            using (DataAccessContext context = new DataAccessContext())
            {
                var lstPr = _mapper.Map<List<ProductDTO>>(_productDAO.GetProductByCateId(id));
                foreach (var pr in lstPr)
                {
                    try
                    {
                        var price = context.ProductDetails.FirstOrDefault(x => x.ProductId == pr.ProductId).ProductDetailPrice;
                        pr.ProductPrice = price;
                        pr.ProductSalePrice = price - (price / 100 * pr.SalePercent);
                    }
                    catch (Exception)
                    {
                        pr.ProductPrice = 0;
                    }
                }

                return lstPr;
            }
        }

        public List<ProductDTO> GetProductsAll()
        {
            return _mapper.Map<List<ProductDTO>>(_productDAO.GetProductsAll());
        }

        public void RemoveSaleProduct(int id)
        {
            _productDAO.RemoveSaleProduct(id);
        }

        public void UpdateSaleProduct(int id, double percent)
        {
            _productDAO.UpdateSaleProduct(id, percent);
        }
    }
}
