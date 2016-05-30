import moment from 'moment';

class LongTimeFormatValueConverter {
  toView(value) {
    return moment(value).format('M/D/YYYY h:mm:ss a');
  }
}

class TimeAgoFormatValueConverter {
  toView(value) {
    return moment(value).fromNow();
  }
}

export {
LongTimeFormatValueConverter,
TimeAgoFormatValueConverter
}