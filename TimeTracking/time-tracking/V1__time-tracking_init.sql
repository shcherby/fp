CREATE TABLE "issue_time_tracking" (
    "id" uuid NOT NULL,
	"issue_id" uuid NOT NULL,
	"time_minutes" int4 NOT NULL,
	"created_at" timestamp NOT NULL,
	"reported_date" timestamp NOT NULL,
    "created_by" varchar(255) NOT NULL,
	"description" varchar(255) NOT NULL,
	CONSTRAINT "PK_issue_time_tracking" PRIMARY KEY ("id")
)
WITH (
  OIDS=FALSE
);
ALTER TABLE issue_time_tracking
  OWNER TO postgres;

CREATE INDEX "IX_issue_time_tracking_issue_id" ON "issue_time_tracking" ("issue_id");

CREATE INDEX "IX_issue_time_tracking_created_by" ON "issue_time_tracking" ("created_by");
