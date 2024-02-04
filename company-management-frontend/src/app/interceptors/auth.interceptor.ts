import { HttpInterceptorFn } from '@angular/common/http';
import { environment } from '../../environments/environment';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  const modifiedRequest = req.clone({ headers: 
    req.headers.append("x-api-key", environment.apiKey)
  });

  return next(modifiedRequest);
};