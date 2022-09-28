import { Group } from "./group";

export interface Activity {
  ActivityId: number;
  StartTime: string;
  EndTime: string;
  WeekdayId: number;
  GroupId?: number;
}
