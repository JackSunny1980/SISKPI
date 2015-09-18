

//js 只能输入数字和小数
function clearNoNum1(obj) {

    //先把非数字的都替换掉，除了数字和.
    obj.value = obj.value.replace(/[^\d.]/g, "");
    //必须保证第一个为数字而不是.
    obj.value = obj.value.replace(/^\./g, "");
    //保证只有出现一个.而没有多个.
    obj.value = obj.value.replace(/\.{2,}/g, ".");
    //保证.只出现一次，而不能出现两次以上
    obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");

    //一期数据计算
    AutoCalcForOne(obj);

 }

//js 只能输入数字和小数
function clearNoNum2(obj) {

    //先把非数字的都替换掉，除了数字和.
    obj.value = obj.value.replace(/[^\d.]/g, "");
    //必须保证第一个为数字而不是.
    obj.value = obj.value.replace(/^\./g, "");
    //保证只有出现一个.而没有多个.
    obj.value = obj.value.replace(/\.{2,}/g, ".");
    //保证.只出现一次，而不能出现两次以上
    obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");


    //二期数据计算
    AutoCalcForTwo(obj);

}


//一期数据计算
function AutoCalcForOne(obj) {


    //第一列相关编辑时，其他值更新
    if (obj.id == "INP101" || obj.id == "INP111" || obj.id == "INP121") {
    var vv = 0.0;
    var obj1 = document.getElementById('INP101');
    var obj2 = document.getElementById('INP111');
    var obj3 = document.getElementById('INP121');

    var obj4 = document.getElementById('INP131');
    
    vv = (parseFloat(obj1.value) + parseFloat(obj2.value) + parseFloat(obj3.value)) / 3.0;
    obj4.value = vv.toFixed(2);

    var obj5 = document.getElementById('INP132');
    
    var obj6 = document.getElementById('INP133');
    
    var obj7 = document.getElementById('INP138');

    var obj8 = document.getElementById('INP139');
    var obj9 = document.getElementById('INP1310');

    vv = parseFloat(obj7.value) * (100 - parseFloat(obj4.value)) / (100 - parseFloat(obj5.value));
    obj8.value = vv.toFixed(2);
    
    vv = parseFloat(obj6.value) * (100 - parseFloat(obj4.value)) / (100 - parseFloat(obj5.value));
    obj9.value = vv.toFixed(2);

    /////////////////////////////////////
    var objcopy = document.getElementById('INP109');
    objcopy.value = obj8.value;
    objcopy = document.getElementById('INP119');
    objcopy.value = obj8.value;
    objcopy = document.getElementById('INP129');
    objcopy.value = obj8.value;

    objcopy = document.getElementById('INP1010');
    objcopy.value = obj9.value;
    objcopy = document.getElementById('INP1110');
    objcopy.value = obj9.value;
    objcopy = document.getElementById('INP1210');
    objcopy.value = obj9.value;

    return;

    }

    //第二列相关编辑时，其他值更新
    if (obj.id == "INP102" || obj.id == "INP112" || obj.id == "INP122") {
        var vv = 0.0;
        var obj1 = document.getElementById('INP102');
        var obj2 = document.getElementById('INP112');
        var obj3 = document.getElementById('INP122');
        
        var obj4 = document.getElementById('INP103');
        var obj5 = document.getElementById('INP113');
        var obj6 = document.getElementById('INP123');

        var obj7 = document.getElementById('INP104');
        var obj8 = document.getElementById('INP114');
        var obj9 = document.getElementById('INP124');

        var obj10 = document.getElementById('INP105');
        var obj11 = document.getElementById('INP115');
        var obj12 = document.getElementById('INP125');         
        

        vv = 100.00 - (parseFloat(obj1.value)+ parseFloat(obj4.value) + parseFloat(obj7.value));
        obj10.value = vv.toFixed(2);
        vv = 100.00 - (parseFloat(obj2.value) + parseFloat(obj5.value) + parseFloat(obj8.value));
        obj11.value = vv.toFixed(2);
        vv = 100.00 - (parseFloat(obj3.value) + parseFloat(obj6.value) + parseFloat(obj9.value));
        obj12.value = vv.toFixed(2);

        var obj13 = document.getElementById('INP132');
        vv = (parseFloat(obj1.value) + parseFloat(obj2.value) + parseFloat(obj3.value)) / 3.0;
        obj13.value = vv.toFixed(2); 

        var obj14 = document.getElementById('INP135');
        vv = (parseFloat(obj10.value) + parseFloat(obj11.value) + parseFloat(obj12.value)) / 3.0;
        obj14.value = vv.toFixed(2);


        /////////////////////////////////////////////
        //重新赋值

        obj4 = document.getElementById('INP131');
        
        obj5 = document.getElementById('INP132');

        obj6 = document.getElementById('INP133');

        obj7 = document.getElementById('INP138');

        obj8 = document.getElementById('INP139');
        obj9 = document.getElementById('INP1310');

        vv = parseFloat(obj7.value) * (100 - parseFloat(obj4.value)) / (100 - parseFloat(obj5.value));
        obj8.value = vv.toFixed(2);

        vv = parseFloat(obj6.value) * (100 - parseFloat(obj4.value)) / (100 - parseFloat(obj5.value));
        obj9.value = vv.toFixed(2);

        /////////////////////////////////////
        var objcopy = document.getElementById('INP109');
        objcopy.value = obj8.value;
        objcopy = document.getElementById('INP119');
        objcopy.value = obj8.value;
        objcopy = document.getElementById('INP129');
        objcopy.value = obj8.value;

        objcopy = document.getElementById('INP1010');
        objcopy.value = obj9.value;
        objcopy = document.getElementById('INP1110');
        objcopy.value = obj9.value;
        objcopy = document.getElementById('INP1210');
        objcopy.value = obj9.value;      

        return;

    }


    //第三列相关编辑时，其他值更新
    if (obj.id == "INP103" || obj.id == "INP113" || obj.id == "INP123") {
        var vv = 0.0;
        var obj1 = document.getElementById('INP102');
        var obj2 = document.getElementById('INP112');
        var obj3 = document.getElementById('INP122');

        var obj4 = document.getElementById('INP103');
        var obj5 = document.getElementById('INP113');
        var obj6 = document.getElementById('INP123');

        var obj7 = document.getElementById('INP104');
        var obj8 = document.getElementById('INP114');
        var obj9 = document.getElementById('INP124');

        var obj10 = document.getElementById('INP105');
        var obj11 = document.getElementById('INP115');
        var obj12 = document.getElementById('INP125');


        vv = 100.00 - (parseFloat(obj1.value)+ parseFloat(obj4.value) + parseFloat(obj7.value));
        obj10.value = vv.toFixed(2);
        vv = 100.00 - (parseFloat(obj2.value) + parseFloat(obj5.value) + parseFloat(obj8.value));
        obj11.value = vv.toFixed(2);
        vv = 100.00 - (parseFloat(obj3.value) + parseFloat(obj6.value) + parseFloat(obj9.value));
        obj12.value = vv.toFixed(2);

        var obj13 = document.getElementById('INP133');
        vv = (parseFloat(obj4.value) + parseFloat(obj5.value) + parseFloat(obj6.value)) / 3.0;
        obj13.value = vv.toFixed(2);

        var obj14 = document.getElementById('INP135');
        vv = (parseFloat(obj10.value) + parseFloat(obj11.value) + parseFloat(obj12.value)) / 3.0;
        obj14.value = vv.toFixed(2);


        /////////////////////////////////////////////
        //重新赋值

        obj4 = document.getElementById('INP131');

        obj5 = document.getElementById('INP132');

        obj6 = document.getElementById('INP133');

        //obj7 = document.getElementById('INP138');

        //obj8 = document.getElementById('INP139');
        obj9 = document.getElementById('INP1310');

        //vv = parseFloat(obj7.value) * (100 - parseFloat(obj4.value)) / (100 - parseFloat(obj5.value));
        //obj8.value = vv.toFixed(2);

        vv = parseFloat(obj6.value) * (100 - parseFloat(obj4.value)) / (100 - parseFloat(obj5.value));
        obj9.value = vv.toFixed(2);

        /////////////////////////////////////
        //var objcopy = document.getElementById('INP109');
        //objcopy.value = obj8.value;
        //objcopy = document.getElementById('INP119');
        //objcopy.value = obj8.value;
        //objcopy = document.getElementById('INP129');
        //objcopy.value = obj8.value;

        var objcopy = document.getElementById('INP1010');
        objcopy.value = obj9.value;
        objcopy = document.getElementById('INP1110');
        objcopy.value = obj9.value;
        objcopy = document.getElementById('INP1210');
        objcopy.value = obj9.value;

        return;

    }


    //第四列相关编辑时，其他值更新
    if (obj.id == "INP104" || obj.id == "INP114" || obj.id == "INP124") {
        var vv = 0.0;
        var obj1 = document.getElementById('INP102');
        var obj2 = document.getElementById('INP112');
        var obj3 = document.getElementById('INP122');

        var obj4 = document.getElementById('INP103');
        var obj5 = document.getElementById('INP113');
        var obj6 = document.getElementById('INP123');

        var obj7 = document.getElementById('INP104');
        var obj8 = document.getElementById('INP114');
        var obj9 = document.getElementById('INP124');

        var obj10 = document.getElementById('INP105');
        var obj11 = document.getElementById('INP115');
        var obj12 = document.getElementById('INP125');


        vv = 100.00 - (parseFloat(obj1.value)+ parseFloat(obj4.value) + parseFloat(obj7.value));
        obj10.value = vv.toFixed(2);
        vv = 100.00 - (parseFloat(obj2.value) + parseFloat(obj5.value) + parseFloat(obj8.value));
        obj11.value = vv.toFixed(2);
        vv = 100.00 - (parseFloat(obj3.value) + parseFloat(obj6.value) + parseFloat(obj9.value));
        obj12.value = vv.toFixed(2);

        var obj13 = document.getElementById('INP134');
        vv = (parseFloat(obj7.value) + parseFloat(obj8.value) + parseFloat(obj9.value)) / 3.0;
        obj13.value = vv.toFixed(2);

        var obj14 = document.getElementById('INP135');
        vv = (parseFloat(obj10.value) + parseFloat(obj11.value) + parseFloat(obj12.value)) / 3.0;
        obj14.value = vv.toFixed(2);

        return;

    }

    //第五列是计算得到的

    //第六列相关编辑时，其他值更新
    if (obj.id == "INP106" || obj.id == "INP116" || obj.id == "INP126") {
        var vv = 0.0;
        var obj1 = document.getElementById('INP106');
        var obj2 = document.getElementById('INP116');
        var obj3 = document.getElementById('INP126');

        var obj4 = document.getElementById('INP136');
        vv = (parseFloat(obj1.value) + parseFloat(obj2.value) + parseFloat(obj3.value)) / 3.0;
        obj4.value = vv.toFixed(2);

        return;

    }


    //第七列相关编辑时，其他值更新
    if (obj.id == "INP107" || obj.id == "INP117" || obj.id == "INP127") {
        var vv = 0.0;
        var obj1 = document.getElementById('INP107');
        var obj2 = document.getElementById('INP117');
        var obj3 = document.getElementById('INP127');

        var obj4 = document.getElementById('INP137');
        vv = (parseFloat(obj1.value) + parseFloat(obj2.value) + parseFloat(obj3.value)) / 3.0;
        obj4.value = vv.toFixed(2);

        return;

    }


    //第八列相关编辑时，其他值更新
    //只看得见第三排的
    if (obj.id == "INP138") {
        var objcopy = document.getElementById('INP108');
        objcopy.value = obj.value;
        objcopy = document.getElementById('INP118');
        objcopy.value = obj.value;
        objcopy = document.getElementById('INP128');
        objcopy.value = obj.value;

        obj4 = document.getElementById('INP131');

        obj5 = document.getElementById('INP132');

        obj8 = document.getElementById('INP139');

        vv = parseFloat(obj.value) * (100 - parseFloat(obj4.value)) / (100 - parseFloat(obj5.value));
        obj8.value = vv.toFixed(2);

        objcopy = document.getElementById('INP109');
        objcopy.value = obj8.value;
        objcopy = document.getElementById('INP119');
        objcopy.value = obj8.value;
        objcopy = document.getElementById('INP129');
        objcopy.value = obj8.value;
        
        

        return;

    }


    //第九列不能输入
    
    //第十列不能输入

}



//二期数据计算    
function AutoCalcForTwo(obj) {

    //第一列相关编辑时，其他值更新
    if (obj.id == "INP201" || obj.id == "INP211" || obj.id == "INP221") {
        var vv = 0.0;
        var obj1 = document.getElementById('INP201');
        var obj2 = document.getElementById('INP211');
        var obj3 = document.getElementById('INP221');

        var obj4 = document.getElementById('INP231');

        vv = (parseFloat(obj1.value) + parseFloat(obj2.value) + parseFloat(obj3.value)) / 3.0;
        obj4.value = vv.toFixed(2);

        var obj5 = document.getElementById('INP232');

        var obj6 = document.getElementById('INP233');

        var obj7 = document.getElementById('INP238');

        var obj8 = document.getElementById('INP239');
        var obj9 = document.getElementById('INP2310');

        vv = parseFloat(obj7.value) * (100 - parseFloat(obj4.value)) / (100 - parseFloat(obj5.value));
        obj8.value = vv.toFixed(2);

        vv = parseFloat(obj6.value) * (100 - parseFloat(obj4.value)) / (100 - parseFloat(obj5.value));
        obj9.value = vv.toFixed(2);

        /////////////////////////////////////
        var objcopy = document.getElementById('INP209');
        objcopy.value = obj8.value;
        objcopy = document.getElementById('INP219');
        objcopy.value = obj8.value;
        objcopy = document.getElementById('INP229');
        objcopy.value = obj8.value;

        objcopy = document.getElementById('INP2010');
        objcopy.value = obj9.value;
        objcopy = document.getElementById('INP2110');
        objcopy.value = obj9.value;
        objcopy = document.getElementById('INP2210');
        objcopy.value = obj9.value;

        return;

    }

    //第二列相关编辑时，其他值更新
    if (obj.id == "INP202" || obj.id == "INP212" || obj.id == "INP222") {
        var vv = 0.0;
        var obj1 = document.getElementById('INP202');
        var obj2 = document.getElementById('INP212');
        var obj3 = document.getElementById('INP222');

        var obj4 = document.getElementById('INP203');
        var obj5 = document.getElementById('INP213');
        var obj6 = document.getElementById('INP223');

        var obj7 = document.getElementById('INP204');
        var obj8 = document.getElementById('INP214');
        var obj9 = document.getElementById('INP224');

        var obj10 = document.getElementById('INP205');
        var obj11 = document.getElementById('INP215');
        var obj12 = document.getElementById('INP225');


        vv = 100.00 - (parseFloat(obj1.value)+ parseFloat(obj4.value) + parseFloat(obj7.value));
        obj10.value = vv.toFixed(2);
        vv = 100.00 - (parseFloat(obj2.value) + parseFloat(obj5.value) + parseFloat(obj8.value));
        obj11.value = vv.toFixed(2);
        vv = 100.00 - (parseFloat(obj3.value) + parseFloat(obj6.value) + parseFloat(obj9.value));
        obj12.value = vv.toFixed(2);

        var obj13 = document.getElementById('INP232');
        vv = (parseFloat(obj1.value) + parseFloat(obj2.value) + parseFloat(obj3.value)) / 3.0;
        obj13.value = vv.toFixed(2);

        var obj14 = document.getElementById('INP235');
        vv = (parseFloat(obj10.value) + parseFloat(obj11.value) + parseFloat(obj12.value)) / 3.0;
        obj14.value = vv.toFixed(2);


        /////////////////////////////////////////////
        //重新赋值

        obj4 = document.getElementById('INP231');

        obj5 = document.getElementById('INP232');

        obj6 = document.getElementById('INP233');

        obj7 = document.getElementById('INP238');

        obj8 = document.getElementById('INP239');
        obj9 = document.getElementById('INP2310');

        vv = parseFloat(obj7.value) * (100 - parseFloat(obj4.value)) / (100 - parseFloat(obj5.value));
        obj8.value = vv.toFixed(2);

        vv = parseFloat(obj6.value) * (100 - parseFloat(obj4.value)) / (100 - parseFloat(obj5.value));
        obj9.value = vv.toFixed(2);

        /////////////////////////////////////
        var objcopy = document.getElementById('INP209');
        objcopy.value = obj8.value;
        objcopy = document.getElementById('INP219');
        objcopy.value = obj8.value;
        objcopy = document.getElementById('INP229');
        objcopy.value = obj8.value;

        objcopy = document.getElementById('INP2010');
        objcopy.value = obj9.value;
        objcopy = document.getElementById('INP2110');
        objcopy.value = obj9.value;
        objcopy = document.getElementById('INP2210');
        objcopy.value = obj9.value;

        return;

    }


    //第三列相关编辑时，其他值更新
    if (obj.id == "INP203" || obj.id == "INP213" || obj.id == "INP223") {
        var vv = 0.0;
        var obj1 = document.getElementById('INP202');
        var obj2 = document.getElementById('INP212');
        var obj3 = document.getElementById('INP222');

        var obj4 = document.getElementById('INP203');
        var obj5 = document.getElementById('INP213');
        var obj6 = document.getElementById('INP223');

        var obj7 = document.getElementById('INP204');
        var obj8 = document.getElementById('INP214');
        var obj9 = document.getElementById('INP224');

        var obj10 = document.getElementById('INP205');
        var obj11 = document.getElementById('INP215');
        var obj12 = document.getElementById('INP225');


        vv = 100.00 - (parseFloat(obj1.value)+ parseFloat(obj4.value) + parseFloat(obj7.value));
        obj10.value = vv.toFixed(2);
        vv = 100.00 - (parseFloat(obj2.value) + parseFloat(obj5.value) + parseFloat(obj8.value));
        obj11.value = vv.toFixed(2);
        vv = 100.00 - (parseFloat(obj3.value) + parseFloat(obj6.value) + parseFloat(obj9.value));
        obj12.value = vv.toFixed(2);

        var obj13 = document.getElementById('INP233');
        vv = (parseFloat(obj4.value) + parseFloat(obj5.value) + parseFloat(obj6.value)) / 3.0;
        obj13.value = vv.toFixed(2);

        var obj14 = document.getElementById('INP235');
        vv = (parseFloat(obj10.value) + parseFloat(obj11.value) + parseFloat(obj12.value)) / 3.0;
        obj14.value = vv.toFixed(2);


        /////////////////////////////////////////////
        //重新赋值

        obj4 = document.getElementById('INP231');

        obj5 = document.getElementById('INP232');

        obj6 = document.getElementById('INP233');

        //obj7 = document.getElementById('INP238');

        //obj8 = document.getElementById('INP239');
        obj9 = document.getElementById('INP2310');

        //vv = parseFloat(obj7.value) * (100 - parseFloat(obj4.value)) / (100 - parseFloat(obj5.value));
        //obj8.value = vv.toFixed(2);

        vv = parseFloat(obj6.value) * (100 - parseFloat(obj4.value)) / (100 - parseFloat(obj5.value));
        obj9.value = vv.toFixed(2);

        /////////////////////////////////////
        //var objcopy = document.getElementById('INP209');
        //objcopy.value = obj8.value;
        //objcopy = document.getElementById('INP219');
        //objcopy.value = obj8.value;
        //objcopy = document.getElementById('INP229');
        //objcopy.value = obj8.value;

        var objcopy = document.getElementById('INP2010');
        objcopy.value = obj9.value;
        objcopy = document.getElementById('INP2110');
        objcopy.value = obj9.value;
        objcopy = document.getElementById('INP2210');
        objcopy.value = obj9.value;

        return;

    }


    //第四列相关编辑时，其他值更新
    if (obj.id == "INP204" || obj.id == "INP214" || obj.id == "INP224") {
        var vv = 0.0;
        var obj1 = document.getElementById('INP202');
        var obj2 = document.getElementById('INP212');
        var obj3 = document.getElementById('INP222');

        var obj4 = document.getElementById('INP203');
        var obj5 = document.getElementById('INP213');
        var obj6 = document.getElementById('INP223');

        var obj7 = document.getElementById('INP204');
        var obj8 = document.getElementById('INP214');
        var obj9 = document.getElementById('INP224');

        var obj10 = document.getElementById('INP205');
        var obj11 = document.getElementById('INP215');
        var obj12 = document.getElementById('INP225');


        vv = 100.00 - (parseFloat(obj1.value)+ parseFloat(obj4.value) + parseFloat(obj7.value));
        obj10.value = vv.toFixed(2);
        vv = 100.00 - (parseFloat(obj2.value) + parseFloat(obj5.value) + parseFloat(obj8.value));
        obj11.value = vv.toFixed(2);
        vv = 100.00 - (parseFloat(obj3.value) + parseFloat(obj6.value) + parseFloat(obj9.value));
        obj12.value = vv.toFixed(2);

        var obj13 = document.getElementById('INP234');
        vv = (parseFloat(obj7.value) + parseFloat(obj8.value) + parseFloat(obj9.value)) / 3.0;
        obj13.value = vv.toFixed(2);

        var obj14 = document.getElementById('INP235');
        vv = (parseFloat(obj10.value) + parseFloat(obj11.value) + parseFloat(obj12.value)) / 3.0;
        obj14.value = vv.toFixed(2);

        return;

    }

    //第五列是计算得到的

    //第六列相关编辑时，其他值更新
    if (obj.id == "INP206" || obj.id == "INP216" || obj.id == "INP226") {
        var vv = 0.0;
        var obj1 = document.getElementById('INP206');
        var obj2 = document.getElementById('INP216');
        var obj3 = document.getElementById('INP226');

        var obj4 = document.getElementById('INP236');
        vv = (parseFloat(obj1.value) + parseFloat(obj2.value) + parseFloat(obj3.value)) / 3.0;
        obj4.value = vv.toFixed(2);

        return;

    }


    //第七列相关编辑时，其他值更新
    if (obj.id == "INP207" || obj.id == "INP217" || obj.id == "INP227") {
        var vv = 0.0;
        var obj1 = document.getElementById('INP207');
        var obj2 = document.getElementById('INP217');
        var obj3 = document.getElementById('INP227');

        var obj4 = document.getElementById('INP237');
        vv = (parseFloat(obj1.value) + parseFloat(obj2.value) + parseFloat(obj3.value)) / 3.0;
        obj4.value = vv.toFixed(2);

        return;

    }


    //第八列相关编辑时，其他值更新
    //只看得见第三排的
    if (obj.id == "INP238") {
        var objcopy = document.getElementById('INP208');
        objcopy.value = obj.value;
        objcopy = document.getElementById('INP218');
        objcopy.value = obj.value;
        objcopy = document.getElementById('INP228');
        objcopy.value = obj.value;

        obj4 = document.getElementById('INP231');

        obj5 = document.getElementById('INP232');

        obj8 = document.getElementById('INP239');

        vv = parseFloat(obj.value) * (100 - parseFloat(obj4.value)) / (100 - parseFloat(obj5.value));
        obj8.value = vv.toFixed(2);

        objcopy = document.getElementById('INP209');
        objcopy.value = obj8.value;
        objcopy = document.getElementById('INP219');
        objcopy.value = obj8.value;
        objcopy = document.getElementById('INP229');
        objcopy.value = obj8.value;

        return;

    }


    //第九列不能输入

    //第十列不能输入

}
