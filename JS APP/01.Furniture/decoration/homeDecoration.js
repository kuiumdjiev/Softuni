import {getFurnitures} from "../helpers/Get/get-furnitures.js"
import { furnitures } from "../template/furnituresTemplate.js";
import { navTemplate } from "../template/navTempalte.js";

export async function homeDecoration(ctx, next) {
    let data =await getFurnitures();
    console.log(data);

    ctx.furnitures  = furnitures(data);
    ctx.nav = navTemplate();
    
    next();
}