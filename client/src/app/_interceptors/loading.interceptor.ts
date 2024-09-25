import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { BuzyService } from '../_services/buzy.service';
import { delay, finalize } from 'rxjs';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const buzyService = inject(BuzyService);

  buzyService.busy();
  return next(req).pipe(
    delay(1000),
    finalize(() =>
    buzyService.idle())
  );
};
