-- --Part 1
SELECT COLUMN_NAME AS "Column Name", DATA_TYPE AS "Data Type"
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'jobs';

-- --Part 2
SELECT Name AS "Employer Name" FROM employers
WHERE employers.Location = "St. Louis City, MO";


-- --Part 3
SELECT jobs.Name AS "Job Name", skills.Name AS "Skill Name"
  FROM jobs
LEFT OUTER JOIN jobskills
  ON jobs.Id = jobskills.JobId
LEFT OUTER JOIN skills
  ON jobskills.SkillId = skills.Id
  WHERE jobskills.SkillId IS NOT NULL