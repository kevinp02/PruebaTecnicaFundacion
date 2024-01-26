import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../auth.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (request.headers.get('skip')) return next.handle(request);

    const usuario = this.authService.usuarioValue;
    const isLoggedIn = usuario && usuario.jwtToken;
    const isApiUrl = request.url.startsWith('http://localhost:4000/api');
    if (isLoggedIn && isApiUrl) {
      request = request.clone({
        setHeaders: { Authorization: `Bearer ${usuario.jwtToken}` },
      });
    }

    return next.handle(request);
  }
}
