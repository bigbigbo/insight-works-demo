import type { EquipmentSyncRecord, SyncStatus, SyncType } from "@/typings/api";
import dayjs from "dayjs";

export class EquipmentSyncRecordEntity implements EquipmentSyncRecord {
  syncType: SyncType;
  status: SyncStatus;
  syncStartTime: string;
  syncEndTime: string;
  equipmentId: string;
  errorMessage?: string;

  constructor(data: Partial<EquipmentSyncRecord>) {
    Object.assign(this, data);
  }

  get formattedSyncStartTime() {
    return dayjs(this.syncStartTime).format("YYYY-MM-DD HH:mm:ss");
  }

  get formattedSyncEndTime() {
    return dayjs(this.syncEndTime).format("YYYY-MM-DD HH:mm:ss");
  }
}
