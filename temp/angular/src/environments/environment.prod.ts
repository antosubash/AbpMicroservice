import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'Tasky',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44348/',
    redirectUri: baseUrl,
    clientId: 'Tasky_App',
    responseType: 'code',
    scope: 'offline_access Tasky',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44331',
      rootNamespace: 'Tasky',
    },
  },
} as Environment;
