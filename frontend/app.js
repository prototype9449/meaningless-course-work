import 'babel-polyfill';
import React from 'react';
import {render} from 'react-dom';

import {Admin, Resource, Delete, englishMessages, jsonRest} from './src/index';
import frenchMessages from 'aor-language-french';

//import addUploadFeature from './addUploadFeature';

import {PostList, PostCreate, PostEdit, PostShow, PostIcon} from './posts';
import {CustomerList, CustomerEdit, CustomerCreate, CustomerIcon} from './customers';

import * as customMessages from './i18n';
import authClient from './authClient';

const messages = {
  fr: {...frenchMessages, ...customMessages.fr},
  en: {...englishMessages, ...customMessages.en},
};

const restClient          = jsonRest('api');
//const uploadCapableClient = addUploadFeature(restClient);

render(
  <Admin authClient={authClient} restClient={restClient} title="Test Page" locale="en" messages={messages}>
    <Resource name="customers" list={CustomerList} create={CustomerCreate} edit={CustomerEdit} remove={Delete}
              icon={CustomerIcon}/>
  </Admin>,
  document.getElementById('root'),
);
