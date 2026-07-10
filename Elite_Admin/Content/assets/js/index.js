$(function() {
    "use strict";

    $('.knob2').knob({
        'format' : function (value) {
            return value + '%';
         }
    });

    // progress bars
    $('.progress .progress-bar').progressbar({
            display_text: 'none'
    });
    $('.sparkline-pie').sparkline('html', {
        type: 'pie',
        offset: 90,
        width: '147px',
        height: '147px',
        sliceColors: ['#02b5b2', '#445771', '#ffcd55']
    })
    $('#sparkline-compositeline').sparkline('html', {
        fillColor: false,
        lineColor: '#445771',
        width: '200px',
        height: '30px',
        lineWidth: '1',
    });
    $('#sparkline-compositeline').sparkline([4, 1, 5, 7, 9, 9, 8, 7, 6, 6, 4, 7, 8, 4, 3, 2, 2, 5, 6, 7], {
        composite: true,
        fillColor: false,
        lineColor: '#02b5b2',
        lineWidth: '1',
        chartRangeMin: 0,
        chartRangeMax: 10
    });
    $('#sparkline-compositeline').sparkline([6, 4, 7, 8, 4, 3, 2, 2, 5, 6, 7, 4, 1, 5, 7, 9, 9, 8, 7, 6], {
        composite: true,
        fillColor: false,
        lineColor: '#ffcd55',
        lineWidth: '1',
        chartRangeMin: 0,
        chartRangeMax: 10
    });

    // =================    
    $('.sparkbar').sparkline('html', { type: 'bar' });

    // notification popup
    //toastr.options.closeButton = true;
    //toastr.options.positionClass = 'toast-bottom-right';    
    //toastr['success']('Hello, welcome to Lucid, a unique admin Template.');
});

