
export interface CompanySearchCriteria {
    companyNo?: number;
    companyName?: string;
    industryId?: number;
    city?: string;
    parentCompany?: string;
    sortByCompanyNameDesc: boolean;
    pageIndex?: number;
    pageSize?: number;
}

export interface ApiPaginationResponse<T> {
    items: T[];
    totalPages: number;
}