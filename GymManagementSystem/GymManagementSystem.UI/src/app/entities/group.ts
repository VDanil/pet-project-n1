import { Activity } from 'src/app/entities/activity';

export interface Group {
  GroupId: number;
  GroupName?: string;
  Description?: string;
  Activities?: Activity[];
}
