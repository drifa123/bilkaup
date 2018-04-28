import { Injectable } from '@angular/core';
declare var escape: any;

@Injectable()
export class MockJwtHelper {

  constructor() { }
  public urlBase64Decode(str: string) {
    let output = str.replace(/-/g, '+').replace(/_/g, '/');
    switch (output.length % 4) {
      case 0: { break; }
      case 2: { output += '=='; break; }
      case 3: { output += '='; break; }
      default: {
        // tslint:disable-next-line:no-string-throw
        throw 'Illegal base64url string!';
      }
    }

    return decodeURIComponent(escape(window.atob(output))); // polifyll https://github.com/davidchambers/Base64.js
  }

  public decodeToken(token: string) {
    const parts = token.split('.');

    if (parts.length !== 3) {
      throw new Error('JWT must have 3 parts');
    }

    const decoded = this.urlBase64Decode(parts[1]);
    if (!decoded) {
      throw new Error('Cannot decode the token');
    }

    return JSON.parse(decoded);
  }

  public getTokenExpirationDate(token: string) {
    let decoded: any;
    decoded = this.decodeToken(token);

    if (typeof decoded.exp === 'undefined') {
      return null;
    }

    const date = new Date(0); // The 0 here is the key, which sets the date to the epoch
    date.setUTCSeconds(decoded.exp);

    return date;
  }

  public isTokenExpired(token: string, offsetSeconds?: number) {
    const date = this.getTokenExpirationDate(token);
    offsetSeconds = offsetSeconds || 0;
    if (date === null) {
      return false;
    }

    // Token expired?
    return !(date.valueOf() > (new Date().valueOf() + (offsetSeconds * 1000)));
  }
}

/**
 * Checks for presence of token and that token hasn't expired.
 * For use with the @CanActivate router decorator and NgIf
 */

export function tokenNotExpired(tokenName?: string, jwt?: string) {

  const authToken: string = tokenName || 'id_token';
  let token: string;

  if (jwt) {
    token = jwt;
  } else {
    token = localStorage.getItem(authToken);
  }

  const jwtHelper = new MockJwtHelper();

  if (!token || jwtHelper.isTokenExpired(token, null)) {
    return false;
  } else {
    return true;
  }
}

