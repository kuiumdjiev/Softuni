import page from "../node_modules/page/page.mjs";

import { homeDecoration } from "../decoraion/homeDecoration.js";
import { home } from "../page/home.js";

import { browseDecoration } from "../decoraion/browseDecoration.js";
import { browse } from "../page/browse.js";

import { registarDecoration } from "../decoraion/registarDecoration.js";
import { registar } from "../page/registar.js";

import { loginDecoration } from "../decoraion/loginDecoration.js";
import { login } from "../page/login.js";

import { createDecoration } from "../decoraion/createDecoration.js";
import { create } from "../page/create.js";

import { detailsDecoration } from "../decoraion/detailsDecoration.js";
import { details } from "../page/details.js";

page('/index.html','/');
page('/','/home')
page('/home',homeDecoration,home);

page('/browse',browseDecoration,browse)

page('/registar',registarDecoration,registar)

page('/login',loginDecoration,login);

page('/create', createDecoration,create);

page('/details/:id',detailsDecoration,details)
page.start();