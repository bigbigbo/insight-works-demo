import { http } from "@/utils/http";
import type { EquipmentSyncRecordPaginatedListApiResponse } from "@/typings/api";

export type EquipmentListParams = {
  pageIndex: number;
  pageSize: number;
};

export const getEquipmentSyncLog = (params: EquipmentListParams) => {
  return http.request<EquipmentSyncRecordPaginatedListApiResponse>(
    "get",
    "/api/equipment-sync/records",
    {
      params
    }
  );
};

export const uploadEquipmentSyncRecord = (data: any) => {
  const formData = new FormData();

  formData.append("equipmentId", data.equipmentId);
  formData.append("syncType", data.syncType);
  formData.append("file", data.file);

  return http.request<any>("post", "/api/equipment-sync/upload/excel", {
    data: formData,
    headers: {
      "Content-Type": "multipart/form-data"
    }
  });
};
