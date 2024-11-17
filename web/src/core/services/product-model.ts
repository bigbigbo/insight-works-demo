import {
  getProductModelList,
  createProductModel,
  updateProductModel,
  deleteProductModel,
  type ProductModelListParams
} from "../datasources/product-model";
import { ProductModelEntity } from "../entities/product-model";

export class ProductModelService {
  static getList = (params: ProductModelListParams) => {
    return getProductModelList(params).then(res => {
      res.data.items =
        res.data?.items?.map(item => new ProductModelEntity(item)) || [];
      return res;
    });
  };
  static create = createProductModel;
  static update = updateProductModel;
  static delete = deleteProductModel;
}
