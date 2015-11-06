using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<DateHistogramAggregation>))]
	public interface IDateHistogramAggregation : IBucketAggregation
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("script")]
		string Script { get; set; }

		[JsonProperty("params")]
		IDictionary<string, object> Params { get; set; }

		[JsonProperty("interval")]
		Union<DateInterval, TimeUnitExpression> Interval { get; set; }

		[JsonProperty("format")]
		string Format { get; set; }

		[JsonProperty("min_doc_count")]
		int? MinimumDocumentCount { get; set; }

		[JsonProperty("time_zone")]
		string TimeZone { get; set; }

		[JsonProperty("factor")]
		int? Factor { get; set; }

		[JsonProperty("offset")]
		string Offset { get; set; }

		[JsonProperty("order")]
		HistogramOrder Order { get; set; }

		[JsonProperty("extended_bounds")]
		ExtendedBounds<DateTime> ExtendedBounds { get; set; }
	}

	public class DateHistogramAggregation : BucketAggregationBase, IDateHistogramAggregation
	{
		public Field Field { get; set; }
		public string Script { get; set; }
		public IDictionary<string, object> Params { get; set; }
		public Union<DateInterval, TimeUnitExpression> Interval { get; set; }
		public string Format { get; set; }
		public int? MinimumDocumentCount { get; set; }
		public string TimeZone { get; set; }
		public int? Factor { get; set; }
		public string Offset { get; set; }
		public HistogramOrder Order { get; set; }
		public ExtendedBounds<DateTime> ExtendedBounds { get; set; }

		internal DateHistogramAggregation() { }

		public DateHistogramAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.DateHistogram = this;
	}

	public class DateHistogramAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<DateHistogramAggregationDescriptor<T>, IDateHistogramAggregation, T>
			, IDateHistogramAggregation
		where T : class
	{
		Field IDateHistogramAggregation.Field { get; set; }

		string IDateHistogramAggregation.Script { get; set; }

		IDictionary<string, object> IDateHistogramAggregation.Params { get; set; }

		Union<DateInterval, TimeUnitExpression> IDateHistogramAggregation.Interval { get; set; }

		string IDateHistogramAggregation.Format { get; set; }

		int? IDateHistogramAggregation.MinimumDocumentCount { get; set; }

		string IDateHistogramAggregation.TimeZone { get; set; }

		int? IDateHistogramAggregation.Factor { get; set; }

		string IDateHistogramAggregation.Offset { get; set; }

		HistogramOrder IDateHistogramAggregation.Order { get; set; }

		ExtendedBounds<DateTime> IDateHistogramAggregation.ExtendedBounds { get; set; }

		public DateHistogramAggregationDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public DateHistogramAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public DateHistogramAggregationDescriptor<T> Script(string script) => Assign(a => a.Script = script);

		public DateHistogramAggregationDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector) =>
			Assign(a => a.Params = paramSelector?.Invoke(new FluentDictionary<string, object>()).NullIfNoKeys());

		public DateHistogramAggregationDescriptor<T> Interval(TimeUnitExpression interval) => Assign(a => a.Interval = interval);

		public DateHistogramAggregationDescriptor<T> Interval(DateInterval interval) =>
			Assign(a => a.Interval = interval);

		public DateHistogramAggregationDescriptor<T> Format(string format) => Assign(a => a.Format = format);

		public DateHistogramAggregationDescriptor<T> MinimumDocumentCount(int minimumDocumentCount) =>
			Assign(a => a.MinimumDocumentCount = minimumDocumentCount);

		public DateHistogramAggregationDescriptor<T> TimeZone(string timeZone) => Assign(a => a.TimeZone = timeZone);

		public DateHistogramAggregationDescriptor<T> Interval(int factor) => Assign(a => a.Factor = factor);

		public DateHistogramAggregationDescriptor<T> Offset(string offset) => Assign(a => a.Offset = offset);

		public DateHistogramAggregationDescriptor<T> Order(HistogramOrder order) => Assign(a => a.Order = order);

		public DateHistogramAggregationDescriptor<T> OrderAscending(string key) =>
			Assign(a => a.Order = new HistogramOrder { Key = key, Order = SortOrder.Descending });

		public DateHistogramAggregationDescriptor<T> OrderDescending(string key) =>
			Assign(a => a.Order = new HistogramOrder { Key = key, Order = SortOrder.Descending });

		public DateHistogramAggregationDescriptor<T> ExtendedBounds(DateTime min, DateTime max) =>
			Assign(a=>a.ExtendedBounds = new ExtendedBounds<DateTime> { Minimum = min, Maximum = max });

	}
}