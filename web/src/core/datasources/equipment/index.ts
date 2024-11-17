import { http } from "@/utils/http";
import type {
  CreateEquipmentDTO,
  DeleteEquipmentDTO,
  EquipmentApiResponse,
  EquipmentPaginatedListApiResponse,
  UpdateEquipmentDTO
} from "@/typings/api";

export type EquipmentListParams = {
  pageIndex: number;
  pageSize: number;
};

export function getEquipmentList(params: EquipmentListParams) {
  return http.request<EquipmentPaginatedListApiResponse>(
    "get",
    "/api/equipments",
    {
      params
    }
  );
}

export function createEquipment(data: CreateEquipmentDTO) {
  return http.request<EquipmentApiResponse>("post", "/api/equipments/create", {
    data
  });
}

export function updateEquipment(data: UpdateEquipmentDTO) {
  return http.request<EquipmentApiResponse>("post", "/api/equipments/update", {
    data
  });
}

export function deleteEquipment(data: DeleteEquipmentDTO) {
  return http.request<EquipmentApiResponse>("post", `/api/equipments/delete`, {
    data
  });
}
