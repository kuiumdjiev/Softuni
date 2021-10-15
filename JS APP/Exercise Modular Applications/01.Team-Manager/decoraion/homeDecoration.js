import { homeTemplate } from "../template/homeTemplate.js";
import { navTemplate } from "../template/navTemplate.js";


export async function homeDecoration(ctx, next) {
    ctx.home  = homeTemplate();
    ctx.nav= navTemplate();
    next();
}