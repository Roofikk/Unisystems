import { PaginationInfo } from "./pagination-info.model";

export interface GetQueryParamsModel {
  pagination: PaginationInfo;
  sortBy: string;
  direction: string;
}

export enum Direction {
  Asc = "asc",
  Desc = "desc"
}
