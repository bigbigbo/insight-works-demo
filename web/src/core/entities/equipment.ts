import type { Equipment, Manufacturer } from "@/typings/api";
import dayjs from "dayjs";

export class EquipmentEntity implements Equipment {
  equipmentCode: string;
  contactPerson?: string | null;
  contactPhone?: string | null;
  createdAt?: string;
  updatedAt?: string;
  manufacturerId?: string;
  manufacturer?: Manufacturer;

  constructor(data: Partial<Equipment>) {
    Object.assign(this, data);
  }

  get formattedCreatedAt() {
    return this.createdAt
      ? dayjs(this.createdAt).format("YYYY-MM-DD HH:mm:ss")
      : "";
  }

  get formattedUpdatedAt() {
    return this.updatedAt
      ? dayjs(this.updatedAt).format("YYYY-MM-DD HH:mm:ss")
      : "";
  }
}
