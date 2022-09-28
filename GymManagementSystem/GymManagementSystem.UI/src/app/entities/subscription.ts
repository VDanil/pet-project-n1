import { Visit } from './visit';
import { Activity } from './activity';

export interface Subscription {
  SubscriptionId: number;
  BuyDate?: string | null;
  StartDate?: string | null;
  DurationInDays: number;
  VisitingAmount: number;

  IsFreezed: boolean;
  FreezeDate?: string | null;
  FreezeDaysAmount: number;

  Price?: number | null;

  Activities?: Activity | null;
  Visits?: Visit | null;
}
