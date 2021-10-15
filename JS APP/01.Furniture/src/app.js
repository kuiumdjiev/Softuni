import page from '../node_modules/page/page.mjs';
import { home } from '../pages/home.js';
import { homeDecoration } from '../decoration/homeDecoration.js';
import { registarDecoration } from '../decoration/registerDecoration.js';
import {register} from '../pages/register.js'
import { loginDecoration } from '../decoration/loginDecoration.js';
import { login } from '../pages/login.js';
import { createFurnitureDecoration } from '../decoration/createFurnitureDecoration.js';
import { createFurniture } from '../pages/createFurniture.js';
import { detailsFurnitureDecoration } from '../decoration/detailsFurnitureDecoration.js';
import { detailsFurniture } from '../pages/detailsFurniture.js';
import { updateFurnitureDecoration } from '../decoration/updateFurnitureDecoration.js';
import { updateFurniture } from '../pages/updateFurniture.js';
import { myFurnitureDecoration } from '../decoration/myFurnituresDecoration.js';
import { myFurniture } from '../pages/myFurniture.js';


page('/index.html','/dashboard');
page('/','/dashboard');
page('/dashboard',homeDecoration,home);
page('/register',registarDecoration,register);
page('/login',loginDecoration,login)
page('/createFurniture',createFurnitureDecoration,createFurniture)
page('/detailsFurniture/:id',detailsFurnitureDecoration,detailsFurniture)
page('/updateFurniture/:id',updateFurnitureDecoration,updateFurniture)
page('/myFurniture',myFurnitureDecoration,myFurniture)
page.start();