function confirmdata()
{
var flag;

    flag=confirm("Are you sure to save changes");
    if(flag==true)
        return true;
    else
        return false;
}

function searchabledropdown() {
    $("select").searchable({
        maxListSize: 200, // if list size are less than maxListSize, show them all
        maxMultiMatch: 300, // how many matching entries should be displayed
        exactMatch: false, // Exact matching on search
        wildcards: true, // Support for wildcard characters (*, ?)
        ignoreCase: true, // Ignore case sensitivity
        latency: 200, // how many millis to wait until starting search
        warnMultiMatch: 'top {0} matches ...',
        warnNoMatch: 'no matches ...',
        zIndex: 'auto'
    });
}

function fillDropdown(ddID, url, dataval) {
     debugger;
    //alert(ddID.id.toString());
    $.ajax({
        type: 'Post',
        url: url,
        dataType: 'json',
        data: { id: dataval },
        success: function (state) {
              debugger;
            // alert('sucess');
            $.each(state, function (index, state) {
                // alert(this.Value.toString() + this.Text.toString());
                ddID.append('<option value="' + this.Value.toString() + '">' + this.Text.toString() + '</option>');
            });
        },

        error: function (ex) {
            alert(ex.statusText.toString());
        }

    });
}

function readURL(input, target) {
    //        debugger;
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            target.attr('src', e.target.result).css('display', 'block');
            //$('#blah').attr('src', e.target.result).css('display','block');
        }
        reader.readAsDataURL(input.files[0]);
    }
}

