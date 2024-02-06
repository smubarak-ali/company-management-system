export interface CompanyDdlDto {
    id: number;
    name: string;
}

export interface CompanyDto extends CompanySaveDto {
    id: number;
    companyNo: number;
    industryName: string;
    level: number;
}

export interface CompanySaveDto {
    companyName: string;
    industryId: number;
    totalEmployees: number;
    city: string;
    parentCompany: string;
}