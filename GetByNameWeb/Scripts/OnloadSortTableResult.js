$.tablesorter.addParser({
	id: 'links',
	is: function (s) {
		return false;
	},
	format: function (s) {
		return s.replace(new RegExp(/<.*?>/), "");
	},
	type: 'text'
});

$(document).ready(function () {
	$("#table_search_result").tablesorter({
		headers: {
			0: {
				sorter: 'links'
			}
		}
	});
}
);