import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'Tasky',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:7000',
    redirectUri: baseUrl,
    clientId: 'Tasky_App',
    responseType: 'code',
    scope: 'offline_access IdentityService AdministrationService SaasService',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:7500',
      rootNamespace: 'Tasky',
    },
    Tasky: {
      url: 'https://localhost:44350',
      rootNamespace: 'Tasky',
    },
  },
} as Environment;
