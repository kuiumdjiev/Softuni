import { getMembers } from "../helpers/Get/get-Members.js";
import { getTeams } from "../helpers/Get/get-Teams.js";
import { browseTemplate } from "../template/browseTemplate.js";


export async function browseDecoration(ctx, next) {
    
    let data = await getTeams();
    console.log(data);
    for (let element in data) {
        let     $count   =await getMembers(data[element]._id);
        data[element].$count=$count.length
    }
    ctx.browse  = browseTemplate(data);
    next();
}