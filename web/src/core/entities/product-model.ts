import type { ProductModel } from "@/typings/api";
import dayjs from "dayjs";

export class ProductModelEntity implements ProductModel {
  modelCode: string;
  modelName: string;
  createdAt?: string;
  updatedAt?: string;

  constructor(data: Partial<ProductModel>) {
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
