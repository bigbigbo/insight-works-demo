import {
  getEquipmentStatusLog,
  getGanttChartData,
  getProductionLog,
  getProductionAvgTime
} from "../datasources/statistics";

export class StatisticsService {
  static getEquipmentStatusLog = getEquipmentStatusLog;
  static getProductionLog = getProductionLog;
  static getGanttChartData = getGanttChartData;
  static getProductionAvgTime = getProductionAvgTime;
}
