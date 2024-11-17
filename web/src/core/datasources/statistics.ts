import type { EquipmentStatusHistoryPaginatedListApiResponse } from "@/typings/api";
import { http } from "@/utils/http";

export interface EquipmentStatusLogQuery {
  equipmentId?: string;
  startTime?: string;
  endTime?: string;
  pageIndex?: number;
  pageSize?: number;
}
export const getEquipmentStatusLog = (params: EquipmentStatusLogQuery) => {
  return http.request<EquipmentStatusHistoryPaginatedListApiResponse>(
    "get",
    "/api/statistics/equipment/status-history",
    {
      params
    }
  );
};
