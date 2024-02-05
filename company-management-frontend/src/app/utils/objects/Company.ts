export interface CompanyDdlDto {
    id: number;
    name: string;
}

export interface CompanyDto {
    id: number;
    companyNo: number;
    companyName: string;
    industryId: number;
    industryName: string;
    totalEmployees: number;
    city: string;
    parentCompany: string;
    level: number;
}