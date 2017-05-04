import { AUTH_LOGIN, AUTH_LOGOUT, AUTH_ERROR, AUTH_CHECK } from './src/index';

// Authenticatd by default
export default (type, params) => {
  if (type === AUTH_LOGIN) {
    const { username } = params;
    localStorage.setItem('username', username);
    localStorage.removeItem('not_authenticated');
    return Promise.resolve();
  }
  if (type === AUTH_LOGOUT) {
    localStorage.setItem('not_authenticated', true);
    localStorage.setItem('username', '');
    return Promise.resolve();
  }
  if (type === AUTH_ERROR) {
    const { status } = params;
    return (status === 401 || status === 403) ? Promise.reject() : Promise.resolve();
  }
  if (type === AUTH_CHECK) {
    return localStorage.getItem('not_authenticated') ? Promise.reject() : Promise.resolve();
  }
  return Promise.reject('Unknown method');
};
