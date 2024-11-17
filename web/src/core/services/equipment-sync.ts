import {
  type EquipmentListParams,
  getEquipmentSyncLog,
  uploadEquipmentSyncRecord
} from "@/core/datasources/equipment-sync";
import { EquipmentSyncRecordEntity } from "../entities/equipment-sync-record";

export class EquipmentSyncService {
  static getList = async (params: EquipmentListParams) => {
    const response = await getEquipmentSyncLog(params);
    response.data.items = response.data.items.map(
      item => new EquipmentSyncRecordEntity(item)
    );

    console.log(response);

    return response;
  };

  static upload = uploadEquipmentSyncRecord;
}
