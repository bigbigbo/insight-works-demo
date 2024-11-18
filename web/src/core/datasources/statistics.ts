import type {
  EquipmentStatusHistoryPaginatedListApiResponse,
  ProductionRecordPagedResultApiResponse
} from "@/typings/api";
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

export interface ProductionLogQuery {
  startTime?: string;
  endTime?: string;
  pageIndex?: number;
  pageSize?: number;
  modelCode?: string;
  equipmentId?: string;
  batchNumber?: string;
  calculateAvgTime?: boolean;
}
export const getProductionLog = (params: ProductionLogQuery) => {
  return http.request<ProductionRecordPagedResultApiResponse>(
    "get",
    "/api/statistics/production/records",
    {
      params
    }
  );
};

export interface GanttChartQuery {
  equipmentId?: string;
  equipmentCode?: string;
  startTime?: string;
  endTime?: string;
}
export const getGanttChartData = (params?: GanttChartQuery) => {
  return http.request<any>("get", "/api/statistics/gantt-chart", {
    params
  });
};

export interface ProductionAvgTimeQuery {
  modelIds: string[];
  startTime?: string;
  endTime?: string;
}
export const getProductionAvgTime = (data?: ProductionAvgTimeQuery) => {
  return http.request<any>("post", "/api/statistics/product-model/stats", {
    data
  });
};
