import { http } from "@/utils/http";
import type {
  CreateManufacturerDTO,
  DeleteManufacturerDTO,
  ManufacturerApiResponse,
  ManufacturerPaginatedListApiResponse,
  UpdateManufacturerDTO
} from "@/typings/api";

export type ManufacturerListParams = {
  pageIndex: number;
  pageSize: number;
};
export function getManufacturerList(params: ManufacturerListParams) {
  return http.request<ManufacturerPaginatedListApiResponse>(
    "get",
    "/api/manufacturers",
    {
      params
    }
  );
}

export function createManufacturer(data: CreateManufacturerDTO) {
  return http.request<ManufacturerApiResponse>(
    "post",
    "/api/manufacturers/create",
    {
      data
    }
  );
}

export function updateManufacturer(data: UpdateManufacturerDTO) {
  return http.request<ManufacturerApiResponse>(
    "post",
    `/api/manufacturers/update`,
    {
      data
    }
  );
}

export function deleteManufacturer(data: DeleteManufacturerDTO) {
  return http.request<any>("post", `/api/manufacturers/delete`, {
    data
  });
}
