export interface APIResponse {
  isOk: boolean;
  code: number;
  message: string;
}
export interface DataResponse<TData> {
  isOk: boolean;
  code: number;
  message: string;
  data: TData;
}



export interface TokenResponse {
  accessToken: string;
  refreshToken: string;
  userId: string;
  login: string;
  userRole: string;
}


export class LoginRequest {

  constructor(l: string, p: string) {
    this.login = l;
    this.password = p;
  }

  login!: string;
  password!: string;
}

export class SignupRequest {
  email!: string;
  login!: string;
  role!: string;
  password!: string;
}

export class RefreshTokenRequest {
  userId!: number;
  refreshToken!: string;
}

export const environment = {


  production: false,


  apiUrl: 'https://localhost:7133'
  //apiUrl: ''


};

export class User {
  id!: number;
  login!: string;
  email!: string;
  role!: string;
  active!: boolean;
  password!: string;
  passwordsalt!: string;
  refreshtokens!: any[];
}

export class Key {
  id!: string;
  expired!: Date;
  username!: string;
}
export enum AddOrUpdate {
  add,
  update
}
export class DialogResult<T>
{
  isOk!: boolean;
  data!: T;
}
export class UpdateKeyRequest {
  Key!: Key;
  Comment!: string;
  CrmTaskId!: string;
  CrmId!: string | undefined;
}


export class mSettings<T>
{
  value!: T;
  key!: string;
}

export class AdminSettings {
  crmUrl!: string;
  token!: string;
  apiKey!: string;
  greetingTgMsg!: string;
  globalTgMsg!: string;
  logKeepDays!: number;

}

export class CryptoSettings {
  pubKey!: string;
  privKey!: string;
}

export class PaymentSettings {
  shopId!: string;
  shopKey!: string;
  emailReciever!: string;
  LastCheck!: string;
  emailSender!: string;
  senderName!: string;
  smtpServer!: string;
  smtpPort!: number;
  smtpUser!: string;
  smtpPassword!: string;
  smtpSSL!: boolean;
}


export class Client {
  id!: number;
  crmId!: string;
  emailMd5Hash!: string;
  isDeleted!: boolean;
  name!: string;
  middleName!: string;
  lastName!: string;
  phone!: string;
  Keys!: Key[];
}


export interface CrmClient {
  id: string;
  name: string;
  midname: string;
  lastname: string;
  description: string;
  email: string;
  phones: ClientPhone[];
}

export interface ClientPhone {
  number: string;
  maskedNumber: string;
  type: ClientPhoneType;
}

export enum ClientPhoneType {
  Mobile,
  Work,
  Home,
  Other
}

export class FetchKeyRequestFilter {
  Email!: string;
  Id!: string;
  DateFrom!: string | null;
  DateTo!: string | null;
  CrmId!: string | null;
}


export class FetchClientRequestFilter {
  Email!: string;
  Name!: string;
  State!: number;
}


export interface KeyHistoryUI {
  key: string;
  crmTaskId: string;
  changed: string;
  user: string;
  prevDate: string | null;
  expired: string;
  comment: string;
}

export class FetchPaymentFilter {
  df!: Date | null;
  dt!: Date | null;
  amount!: number;
  amountCompare!: AmountCompare;
  transactionId!: string;
  data!: string;
}

export enum AmountCompare {
  All,
  More,
  Less,
  Equal
}

export interface Amount {
  id: number|null;
  value: string | null;
  currency: string | null;
}

export interface AuthorizationDetails {
  id: number|null;
  rrn: string | null;
  auth_code: string | null;
}

export interface CancellationDetails {
  id: number | null;
  party: string | null;
  reason: string | null;
}

export interface Card {
  id: number | null;
  first6: string | null;
  last4: string | null;
  expiry_year: string | null;
  expiry_month: string | null;
  card_type: string | null;
  issuer_country: string | null;
}

export interface IncomeAmount {
  id: number | null;
  value: string | null;
  currency: string | null;
}

export interface PaymentInfo {
  id: string | null;
  status: string | null;
  amount: Amount | null;
  income_amount: IncomeAmount | null;
  recipient: Recipient | null;
  payment_method: PaymentMethod | null;
  captured_at: string | null;
  created_at: string | null;
  test: boolean | null;
  refunded_amount: RefundedAmount | null;
  paid: boolean | null;
  refundable: boolean | null;
  metadata: Metadata | null;
  authorization_details: AuthorizationDetails | null;
  description: string | null;
  cancellation_details: CancellationDetails | null;
  emailSend: boolean | null;
}

export interface Metadata {
  id: number;
  orderDetails: string | null;
  cps_phone: string | null;
  custName: string | null;
  cms_name: string | null;
  cps_email: string | null;
  module_version: string | null;
  wp_user_id: string | null;
}

export interface PaymentMethod {
  id: string | null;
  type: string | null;
  saved: boolean | null;
  title: string | null;
  card: Card | null;
}

export interface Recipient {
  id: number | null;
  account_id: string | null;
  gateway_id: string | null;
}

export interface RefundedAmount {
  id: number | null;
  value: string | null;
  currency: string | null;
}

export interface PaymentData {
  type: string | null;
  items: PaymentItem[];
}

export interface ThreeDSecure {
  applied: boolean|null;
  method_completed: boolean | null;
  challenge_completed: boolean | null;
}


export class Speciality {
  id!: number;
  name!: string;
  doctors: Doctor[]=[];
}


export class DoctorRequest {
  photo: string;
  summary: string;
  empHistory: string;
  degree: string;
  name: string;
  lastname: string;
  middlename: string;
  startWork: number;
  about: string;
  email: string;
  specs: number[];

  constructor()
  {
    this.photo = "";
  }
}


export class Doctor {
  id: number;
  name: string;
  lastname: string;
  middlename: string;
  startWork: number;
  photo: string;
  summaryPath: string;
  empHistoryPath: string;
  degreePath: string;
  specialties: Speciality[];
  about: string;
  email: string;
  confirm: boolean;
  user: User;
}
