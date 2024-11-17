import { http } from "@/utils/http";
import type {
  ProductModelApiResponse,
  ProductModelPaginatedListApiResponse,
  CreateProductDTO,
  UpdateProductDTO,
  DeleteProductDTO
} from "@/typings/api";

export type ProductModelListParams = {
  pageIndex: number;
  pageSize: number;
};

export function getProductModelList(params: ProductModelListParams) {
  return http.request<ProductModelPaginatedListApiResponse>(
    "get",
    "/api/products",
    {
      params
    }
  );
}

export function createProductModel(data: CreateProductDTO) {
  return http.request<ProductModelApiResponse>("post", "/api/products/create", {
    data
  });
}

export function updateProductModel(data: UpdateProductDTO) {
  return http.request<ProductModelApiResponse>("post", "/api/products/update", {
    data
  });
}

export function deleteProductModel(data: DeleteProductDTO) {
  return http.request<ProductModelApiResponse>("post", "/api/products/delete", {
    data
  });
}
