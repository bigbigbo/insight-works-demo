/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

export interface CreateEquipmentDTO {
  equipmentCode?: string | null;
  /** @format uuid */
  manufacturerId?: string;
  contactPerson?: string | null;
  contactPhone?: string | null;
}

export interface CreateManufacturerDTO {
  manufacturerCode?: string | null;
  name?: string | null;
  address?: string | null;
  contactPerson?: string | null;
  contactPhone?: string | null;
}

export interface CreateProductDTO {
  modelCode?: string | null;
  description?: string | null;
}

export interface DeleteEquipmentDTO {
  /** @format uuid */
  id?: string;
}

export interface DeleteManufacturerDTO {
  /** @format uuid */
  id?: string;
}

export interface DeleteProductDTO {
  /** @format uuid */
  id?: string;
}

export interface Equipment {
  /** @format uuid */
  id?: string;
  /**
   * @minLength 1
   * @maxLength 50
   */
  equipmentCode: string;
  /** @maxLength 50 */
  contactPerson?: string | null;
  /** @maxLength 20 */
  contactPhone?: string | null;
  /** @format date-time */
  createdAt?: string;
  /** @format date-time */
  updatedAt?: string;
  /** @format uuid */
  manufacturerId?: string;
  manufacturer?: Manufacturer;
}

export interface EquipmentApiResponse {
  success?: boolean;
  message?: string | null;
  data?: Equipment;
  /** @format date-time */
  timestamp?: string;
}

export interface EquipmentInfo {
  /** @format uuid */
  id?: string;
  equipmentCode?: string | null;
  manufacturerName?: string | null;
}

export interface EquipmentPaginatedList {
  items?: Equipment[] | null;
  /** @format int32 */
  pageIndex?: number;
  /** @format int32 */
  pageSize?: number;
  /** @format int32 */
  totalCount?: number;
  /** @format int32 */
  totalPages?: number;
  hasPreviousPage?: boolean;
  hasNextPage?: boolean;
}

export interface EquipmentPaginatedListApiResponse {
  success?: boolean;
  message?: string | null;
  data?: EquipmentPaginatedList;
  /** @format date-time */
  timestamp?: string;
}

export interface EquipmentProductionStat {
  /** @format uuid */
  equipmentId?: string;
  equipmentCode?: string | null;
  manufacturerName?: string | null;
  /** @format int32 */
  batchCount?: number;
  /** @format double */
  avgProductionTime?: number;
}

/** @format int32 */
export enum EquipmentStatus {
  Value0 = 0,
  Value1 = 1,
  Value2 = 2
}

export interface EquipmentStatusHistory {
  /** @format uuid */
  id?: string;
  /** @format uuid */
  equipmentId?: string;
  status: EquipmentStatus;
  /** @format date-time */
  statusChangeTime: string;
  /** @maxLength 50 */
  executedBy?: string | null;
  equipment?: Equipment;
}

export interface EquipmentStatusHistoryPaginatedList {
  items?: EquipmentStatusHistory[] | null;
  /** @format int32 */
  pageIndex?: number;
  /** @format int32 */
  pageSize?: number;
  /** @format int32 */
  totalCount?: number;
  /** @format int32 */
  totalPages?: number;
  hasPreviousPage?: boolean;
  hasNextPage?: boolean;
}

export interface EquipmentStatusHistoryPaginatedListApiResponse {
  success?: boolean;
  message?: string | null;
  data?: EquipmentStatusHistoryPaginatedList;
  /** @format date-time */
  timestamp?: string;
}

export interface EquipmentSyncRecord {
  /** @format uuid */
  id?: string;
  /** @format uuid */
  equipmentId?: string;
  syncType: SyncType;
  /** @format date-time */
  syncStartTime?: string;
  /** @format date-time */
  syncEndTime?: string | null;
  status: SyncStatus;
  errorMessage?: string | null;
  /** @format date-time */
  createdAt?: string;
  equipment?: Equipment;
}

export interface EquipmentSyncRecordPaginatedList {
  items?: EquipmentSyncRecord[] | null;
  /** @format int32 */
  pageIndex?: number;
  /** @format int32 */
  pageSize?: number;
  /** @format int32 */
  totalCount?: number;
  /** @format int32 */
  totalPages?: number;
  hasPreviousPage?: boolean;
  hasNextPage?: boolean;
}

export interface EquipmentSyncRecordPaginatedListApiResponse {
  success?: boolean;
  message?: string | null;
  data?: EquipmentSyncRecordPaginatedList;
  /** @format date-time */
  timestamp?: string;
}

export interface GanttChartDTO {
  equipment?: EquipmentInfo;
  statusRecords?: StatusRecord[] | null;
  productionRecords?: ProductionGanttRecord[] | null;
}

export interface GanttChartDTOListApiResponse {
  success?: boolean;
  message?: string | null;
  data?: GanttChartDTO[] | null;
  /** @format date-time */
  timestamp?: string;
}

export interface Manufacturer {
  /** @format uuid */
  id?: string;
  /**
   * @minLength 1
   * @maxLength 50
   */
  manufacturerCode: string;
  /**
   * @minLength 1
   * @maxLength 100
   */
  name: string;
  /** @maxLength 200 */
  address?: string | null;
  /** @maxLength 50 */
  contactPerson?: string | null;
  /** @maxLength 20 */
  contactPhone?: string | null;
  /** @format date-time */
  createdAt?: string;
  /** @format date-time */
  updatedAt?: string;
}

export interface ManufacturerApiResponse {
  success?: boolean;
  message?: string | null;
  data?: Manufacturer;
  /** @format date-time */
  timestamp?: string;
}

export interface ManufacturerPaginatedList {
  items?: Manufacturer[] | null;
  /** @format int32 */
  pageIndex?: number;
  /** @format int32 */
  pageSize?: number;
  /** @format int32 */
  totalCount?: number;
  /** @format int32 */
  totalPages?: number;
  hasPreviousPage?: boolean;
  hasNextPage?: boolean;
}

export interface ManufacturerPaginatedListApiResponse {
  success?: boolean;
  message?: string | null;
  data?: ManufacturerPaginatedList;
  /** @format date-time */
  timestamp?: string;
}

export interface ObjectApiResponse {
  success?: boolean;
  message?: string | null;
  data?: any;
  /** @format date-time */
  timestamp?: string;
}

export interface ProductModel {
  /** @format uuid */
  id?: string;
  /**
   * @minLength 1
   * @maxLength 100
   */
  modelCode: string;
  /** @maxLength 200 */
  description?: string | null;
  /** @format date-time */
  createdAt?: string;
  productionRecords?: ProductionRecord[] | null;
}

export interface ProductModelApiResponse {
  success?: boolean;
  message?: string | null;
  data?: ProductModel;
  /** @format date-time */
  timestamp?: string;
}

export interface ProductModelPaginatedList {
  items?: ProductModel[] | null;
  /** @format int32 */
  pageIndex?: number;
  /** @format int32 */
  pageSize?: number;
  /** @format int32 */
  totalCount?: number;
  /** @format int32 */
  totalPages?: number;
  hasPreviousPage?: boolean;
  hasNextPage?: boolean;
}

export interface ProductModelPaginatedListApiResponse {
  success?: boolean;
  message?: string | null;
  data?: ProductModelPaginatedList;
  /** @format date-time */
  timestamp?: string;
}

export interface ProductModelStatDTO {
  /** @format uuid */
  modelId?: string;
  modelCode?: string | null;
  description?: string | null;
  equipmentStats?: EquipmentProductionStat[] | null;
}

export interface ProductModelStatDTOListApiResponse {
  success?: boolean;
  message?: string | null;
  data?: ProductModelStatDTO[] | null;
  /** @format date-time */
  timestamp?: string;
}

export interface ProductModelStatQueryDTO {
  modelIds?: string[] | null;
  /** @format date-time */
  startTime?: string | null;
  /** @format date-time */
  endTime?: string | null;
}

export interface ProductionData {
  /** @format uuid */
  productModelId?: string;
  batchNumber?: string | null;
  /** @minLength 1 */
  preLength: string;
  /** @minLength 1 */
  preWidth: string;
  /** @minLength 1 */
  preHeight: string;
  /** @minLength 1 */
  postLength: string;
  /** @minLength 1 */
  postWidth: string;
  /** @minLength 1 */
  postHeight: string;
  /** @format date-time */
  productionStartTime?: string;
  /** @format date-time */
  productionEndTime?: string;
}

export interface ProductionGanttRecord {
  modelCode?: string | null;
  batchNumber?: string | null;
  /** @format date-time */
  productionStartTime?: string;
  /** @format date-time */
  productionEndTime?: string;
}

export interface ProductionRecord {
  /** @format uuid */
  id?: string;
  /** @format uuid */
  equipmentId?: string;
  /** @format uuid */
  productModelId?: string;
  /**
   * @minLength 1
   * @maxLength 100
   */
  batchNumber: string;
  /**
   * @minLength 1
   * @maxLength 50
   */
  preLength: string;
  /**
   * @minLength 1
   * @maxLength 50
   */
  preWidth: string;
  /**
   * @minLength 1
   * @maxLength 50
   */
  preHeight: string;
  /**
   * @minLength 1
   * @maxLength 50
   */
  postLength: string;
  /**
   * @minLength 1
   * @maxLength 50
   */
  postWidth: string;
  /**
   * @minLength 1
   * @maxLength 50
   */
  postHeight: string;
  /** @format date-time */
  productionStartTime?: string;
  /** @format date-time */
  productionEndTime?: string;
  equipment?: Equipment;
  productModel?: ProductModel;
}

export interface ProductionRecordDTO {
  /** @format uuid */
  id?: string;
  /** @format uuid */
  equipmentId?: string;
  equipmentCode?: string | null;
  manufacturerName?: string | null;
  /** @format uuid */
  productModelId?: string;
  modelCode?: string | null;
  batchNumber?: string | null;
  preLength?: string | null;
  preWidth?: string | null;
  preHeight?: string | null;
  postLength?: string | null;
  postWidth?: string | null;
  postHeight?: string | null;
  /** @format date-time */
  productionStartTime?: string;
  /** @format date-time */
  productionEndTime?: string;
}

export interface ProductionRecordPagedResult {
  items?: ProductionRecordDTO[] | null;
  /** @format int32 */
  totalCount?: number;
  /** @format int32 */
  pageIndex?: number;
  /** @format int32 */
  pageSize?: number;
  /** @format int32 */
  totalPages?: number;
  hasPreviousPage?: boolean;
  hasNextPage?: boolean;
  summary?: ProductionSummary;
}

export interface ProductionRecordPagedResultApiResponse {
  success?: boolean;
  message?: string | null;
  data?: ProductionRecordPagedResult;
  /** @format date-time */
  timestamp?: string;
}

export interface ProductionSummary {
  /** @format double */
  avgProductionTime?: number;
}

export interface StatusData {
  status?: string | null;
  /** @format date-time */
  statusChangeTime?: string;
  executedBy?: string | null;
}

export interface StatusRecord {
  status?: EquipmentStatus;
  /** @format date-time */
  statusChangeTime?: string;
  executedBy?: string | null;
}

export interface SyncCommandDTO {
  /** @format uuid */
  equipmentId?: string;
  syncType?: SyncType;
  /** @format date-time */
  startTime?: string;
  /** @format date-time */
  endTime?: string;
}

/** @format int32 */
export enum SyncStatus {
  Value0 = 0,
  Value1 = 1
}

/** @format int32 */
export enum SyncType {
  Value0 = 0,
  Value1 = 1
}

export interface UpdateEquipmentDTO {
  /** @format uuid */
  id?: string;
  equipmentCode?: string | null;
  /** @format uuid */
  manufacturerId?: string;
  contactPerson?: string | null;
  contactPhone?: string | null;
}

export interface UpdateManufacturerDTO {
  /** @format uuid */
  id?: string;
  manufacturerCode?: string | null;
  name?: string | null;
  address?: string | null;
  contactPerson?: string | null;
  contactPhone?: string | null;
}

export interface UpdateProductDTO {
  /** @format uuid */
  id?: string;
  modelCode?: string | null;
  description?: string | null;
}

export interface UploadProductionDataDTO {
  /** @format uuid */
  equipmentId?: string;
  productionList?: ProductionData[] | null;
}

export interface UploadStatusDataDTO {
  /** @format uuid */
  equipmentId?: string;
  statusList?: StatusData[] | null;
}
