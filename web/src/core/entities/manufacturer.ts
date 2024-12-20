import dayjs from "dayjs";
import type { Manufacturer } from "@/typings/api";

export class ManufacturerEntity implements Manufacturer {
  id?: string;
  manufacturerCode: string;
  name: string;
  address?: string | null;
  contactPerson?: string | null;
  contactPhone?: string | null;
  createdAt?: string;
  updatedAt?: string;

  constructor(data: Partial<Manufacturer>) {
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
